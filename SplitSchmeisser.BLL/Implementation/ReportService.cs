using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SplitSchmeisser.BLL.Implementation
{
    public class ReportService : IReportService
    {
        private IGenericRepository<Operation> operationRepository;
        private IGenericRepository<Group> groupRepository;

        private IUserService userService;
        private IGroupService groupService;

        public ReportService(IGenericRepository<Operation> operationRepository,
            IGenericRepository<Group> groupRepository,
            IUserService userService,
            IGroupService groupService)
        {
            this.operationRepository = operationRepository;
            this.groupRepository = groupRepository;
            this.userService = userService;
            this.groupService = groupService;
        }

        public async Task<byte[]> GenerateDebtsReport(int id)
        {
            var group = await this.groupService.GetGroupById(id);
            return Serialize<List<Debt>>(group.UserDebts);
        }

        public async Task<byte[]> GenerateGroupReport(ReportRequest request)
        {
            var groupDto = await this.groupService.GetGroupById(request.Id);
            groupDto.Operations = groupDto.Operations.Where(x => x.DateOfLoan >= request.StartDate && x.DateOfLoan <= request.EndDate).ToList();

            return Serialize<GroupDTO>(groupDto);
        }

        public async Task<byte[]> GenerateGroupsReport(ReportRequest request)
        {
            var currUser = this.userService.GetCurrUser();

            var gr = await this.groupService.GetGroups();
            var groups = gr.Where(x => x.Users.Any(u => u.Id == currUser.Id)).ToList();

            groups.ForEach(x => x.Operations = x.Operations.Where(o => o.DateOfLoan >= request.StartDate
                                        && o.DateOfLoan <= request.EndDate).ToList());

            return Serialize<List<GroupDTO>>(groups);
        }

        public async Task<byte[]> GenerateOperationReport(int operationId)
        {
            var operation = await this.operationRepository.GetById(operationId);
            var operationDto = OperationDTO.FromEntity(operation);

            return Serialize<OperationDTO>(operationDto);
        }

        public async Task<byte[]> GenerateOperationsReport(ReportRequest request)
        {
            var group = await this.groupRepository.GetById(request.Id);
            var operations = group.Operations.Where(x => x.DateOfLoan >= request.StartDate && x.DateOfLoan <= request.EndDate)
                .Select(x => OperationDTO.FromEntity(x))
                .ToList();

            return Serialize<List<OperationDTO>>(operations);
        }

        private byte[] Serialize<T>(T obj)
        {

            XmlSerializer xs = new XmlSerializer(typeof(T));

            byte[] bytes = null;

            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, obj);
                bytes = ms.ToArray();
            }

            return bytes;
        }
    }
}
