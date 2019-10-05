using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
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


        public ReportService(IGenericRepository<Operation> operationRepository,
            IGenericRepository<Group> groupRepository,
            IUserService userService)
        {
            this.operationRepository = operationRepository;
            this.groupRepository = groupRepository;
            this.userService = userService;
        }

        public async Task<byte[]> GenerateDebtsReport(int id)
        {
            var group = await this.groupRepository.GetById(id);
            var userDebts = await this.userService.GetUserDebtsByGroupPerUrers(GroupDTO.FromEntity(group));

            XmlSerializer xs = new XmlSerializer(typeof(List<Debt>));

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, userDebts);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public async Task<byte[]> GenerateGroupReport(ReportRequest request)
        {
            var group = await this.groupRepository.GetById(request.Id);
            var groupDto = GroupDTO.FromEntity(group);
            groupDto.UserDebts = await this.userService.GetUserDebtsByGroupPerUrers(groupDto);
            groupDto.Operations = groupDto.Operations.Where(x => x.DateOfLoan >= request.StartDate && x.DateOfLoan <= request.EndDate).ToList();

            XmlSerializer xs = new XmlSerializer(typeof(GroupDTO));

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, groupDto);
                bytes = ms.ToArray();
            }

            return bytes;

        }

        public async Task<byte[]> GenerateGroupsReport(ReportRequest request)
        {
            var currUser = this.userService.GetCurrUser();

            var groups = this.groupRepository.GetAll()
                .ToList()
                .Where(x => x.Users.Any(u => u.Id == currUser.Id))
                .Select(x => GroupDTO.FromEntity(x))
                .ToList();

            groups.ForEach(x => x.Operations 
                = x.Operations.Where(o => o.DateOfLoan >= request.StartDate
                                        && o.DateOfLoan <= request.EndDate).ToList());

            XmlSerializer xs = new XmlSerializer(typeof(List<GroupDTO>));

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, groups);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public async Task<byte[]> GenerateOperationReport(int operationId)
        {
            var operation = await this.operationRepository.GetById(operationId);
            var operationDto = OperationDTO.FromEntity(operation);

            XmlSerializer xs = new XmlSerializer(typeof(OperationDTO));

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, operationDto);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public async Task<byte[]> GenerateOperationsReport(ReportRequest request)
        {
            var group = await this.groupRepository.GetById(request.Id);
            var operations = group.Operations.Select(x => OperationDTO.FromEntity(x))
                .Where(x => x.DateOfLoan >= request.StartDate && x.DateOfLoan <= request.EndDate)
                .ToList(); 
            
            XmlSerializer xs = new XmlSerializer(typeof(List<OperationDTO>));

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, operations);
                bytes = ms.ToArray();
            }

            return bytes;
            
        }
    }
}
