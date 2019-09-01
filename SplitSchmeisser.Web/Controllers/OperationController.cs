using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SplitSchmeisser.BLL;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.Web.Models;

namespace SplitSchmeisser.Web.Controllers
{
    public class OperationController : Controller
    {
        IGroupService groupService;
        IUserService userService;
        IOperationService operationService;

        public OperationController(IGroupService groupService,
            IUserService userService,
            IOperationService operationService)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.operationService = operationService;
        }

        public IActionResult Create(int id)
        {
            var user = userService.GetUsers()
                .ToList()
                .First(x => x.Name == CurrentUserService.UserName);

            var op = new CreateOperationModel
            {
                GroupID = id,
                OwnerID = user.Id
            };

            return View(op);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOperationModel op)
        {
            await this.operationService.CreateOperation(
                op.OwnerID,
                op.GroupID,
                op.Amount,
                op.Description
             );

            return RedirectToAction("Details",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Group",
                        action = "Details",
                        Id = op.GroupID
                    }));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var groupId = this.operationService.GetOperations()
                .Where(x => x.Id == id)
                .First().Group.Id;

            await this.operationService.DeleteAsync(id);

            var opCount = groupService
                .GetAllOperationsByGroup(groupId)
                .Result.Count;

            if (opCount > 0)
            {
                return RedirectToAction("Details",
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Group",
                            action = "Details",
                            Id = groupId
                        }));
            }
            else {

                await this.groupService.Delete(groupId);
                return RedirectToRoute("Default", new {
                    controller = "",
                    action = ""
                });
            }
        }
    }
}