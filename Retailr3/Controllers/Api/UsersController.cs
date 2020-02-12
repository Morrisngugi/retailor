//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Core.Models;
//using Core.Models.IdentityModels;
//using Core.Services;
//using Core.Utilities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;

//namespace LendingWeb.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly ISmsSenderJob _smsSenderService;
//        private readonly IPasswordGeneratorService _passwdGenService;
//        private readonly IOptions<AppConfig> _appConfig;
//        public UsersController(ISmsSenderJob smsSenderService,IOptions<AppConfig> appConfig,IPasswordGeneratorService passwdGenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _smsSenderService = smsSenderService;
//            _passwdGenService = passwdGenService;
//            _appConfig = appConfig;
//        }

//        [HttpPost("uploadentity")]
//        public async Task<IActionResult> UploadEntity([FromBody] EntityUploadRequest entitiesUploadRequest)
//        {
//            var reply = new EntitiesUploadResponse
//            {
//                ReferenceId = Guid.NewGuid().ToString(),
//                ResponseCode = "201",
//                ResponseMessage = "Your Request was recceived But Never Processed"
//            };

//            if (!ModelState.IsValid)
//            {
//                reply.ReferenceId = Guid.NewGuid().ToString();
//                reply.ResponseCode = "400";
//                reply.ResponseMessage = "Invalid Request";
//                return Ok(reply);
//            }
//            //check if account with provided email already exist
//            var existingUser = await _userManager.FindByEmailAsync(entitiesUploadRequest.emailAddress);
//            if (existingUser != null)
//            {
//                reply.ResponseMessage = $"User with Email { entitiesUploadRequest.emailAddress} already exists";
//                return Ok(reply);
//            }

//            if (entitiesUploadRequest.anchor)
//            {
//                try
//                {

//                    var entity_type = "Anchor";
//                    var role_name = GenerateRoleName(entity_type);
//                    // check for validity of the request payload
//                    var user = new ApplicationUser
//                    {
//                        UserName = entitiesUploadRequest.entityName.ToLower().Replace(' ','_'),
//                        Email = entitiesUploadRequest.emailAddress,
//                        PhoneNumber = entitiesUploadRequest.phoneNumber,
//                        FirstName = entitiesUploadRequest.entityName,
//                        LastName = entitiesUploadRequest.entityName,
//                        RoleName = role_name
//                    };

//                    //generate random password
//                    var password = _passwdGenService.GenerateRandomPassword();
//                    var result = await _userManager.CreateAsync(user, password);
//                    if (result.Succeeded)
//                    {
//                        var _role = await _roleManager.FindByNameAsync(user.RoleName);
//                        if (_role != null)
//                        {
//                            await _userManager.AddToRoleAsync(user, _role.NormalizedName);
//                        }


//                        //create a merchant model using identityid from the previous record
//                        Merchant merchant = new Merchant
//                        {
//                            AcceptedTerms = true,
//                            AnchorId = entitiesUploadRequest.entityId,
//                            Email = entitiesUploadRequest.emailAddress,
//                            EntityId = entitiesUploadRequest.entityId,
//                            IdentityId = user.Id,
//                            Name = entitiesUploadRequest.entityName,
//                            PhoneNumber = entitiesUploadRequest.phoneNumber,
//                            SupplierId = entitiesUploadRequest.entityId
//                        };


//                        var result1 = await _registrService.AddMerchant(merchant);

//                        if (result1.Success)
//                        {

//                            reply.ResponseCode = "200";
//                            reply.ReferenceId = Guid.NewGuid().ToString();
//                            reply.ResponseMessage = "Account Creation Processed Successfull. ";

//                            var request = HttpContext.Request;
//                            //var BaseUri = string.Concat(request.Scheme, "://", _appConfig.Value.pa);
//                            //var link = BaseUri;
//                            var link = _appConfig.Value.NotifyrBaseUrl;
//                            string message =
//                                string.Format("Dear {0}, Your account has been created with username: {1} and password: {2} on the Stanbic Registration Portal. Opt into this service by clicking:  " + link,
//                                    entitiesUploadRequest.entityName, user.UserName, password);
//                            //send sms notification
//                            var entityNotification = new NotificationRequest
//                            {
//                                EmailId = entitiesUploadRequest.emailAddress,
//                                Message = message,
//                                MobileNumber = entitiesUploadRequest.phoneNumber,
//                                ReferenceId = Guid.NewGuid().ToString(),
//                                SendEmail = true,
//                                SendSMS = true,
//                                Subject = $"Entity Creation Notification"
//                            };


//                            var notification = _stanbicService.SendNotification(entityNotification);

//                            if (notification.Success && notification.Data.ResponseCode.Equals("00"))
//                            {
//                                var additionalMessage = "Entity Notification Send Successfull. ";
//                                reply.ResponseMessage += additionalMessage;
//                                //send subsequent notification to stanbic admins
//                                string EmailMessage = $"Account for Anchor {entitiesUploadRequest.entityName} has been successfully created on supplychain portal";
//                                List<string> adminAddresses = new List<string>();
//                                adminAddresses.Add("WekesaC@stanbic.com");
//                                adminAddresses.Add("nyanarog@stanbic.com");
//                                adminAddresses.Add("mbuguam@stanbic.com");

//                                var emailNotification = new NotificationRequest
//                                {
//                                    EmailId = $"{adminAddresses[0]},{adminAddresses[1]},{adminAddresses[2]}",
//                                    Message = EmailMessage,
//                                    MobileNumber = entitiesUploadRequest.phoneNumber,
//                                    ReferenceId = Guid.NewGuid().ToString(),
//                                    SendEmail = true,
//                                    SendSMS = false,
//                                    Subject = $"Entity Creation Notification"
//                                };

//                                var adminNotification = _stanbicService.SendNotification(emailNotification);
//                                if (notification.Success && notification.Data.ResponseCode.Equals("00"))
//                                {
//                                    var additionalMessage1 = "Administrators Notification Send Successfull. ";
//                                    reply.ResponseMessage += additionalMessage1;
//                                }
//                                else
//                                {
//                                    var additionalMessage2 = "Administrators Notification Failed. ";
//                                    reply.ResponseMessage += additionalMessage2;
//                                }

//                            }
//                            else
//                            {
//                                var additionalMessage = " Notification Failed. ";
//                                reply.ResponseMessage += additionalMessage;
//                            }

//                        }
//                        else
//                        {
//                            reply.ResponseCode = "500";
//                            reply.ReferenceId = Guid.NewGuid().ToString();
//                            reply.ResponseMessage = "An Error Occured.";
//                        }

//                    }
//                }
//                catch (Exception ex)
//                {
//                    reply.ResponseCode = "500";
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseMessage = "An Error Occured while processing Anchor Account. " + ex.Message;
//                }

//            }
//            else
//            {
//                try
//                {
//                    List<Merchant> entityList = new List<Merchant>();
//                    var entity_type = "Merchant";
//                    var role_name = GenerateRoleName(entity_type);
//                    // check for validity of the request payload
//                    var user = new ApplicationUser
//                    {
//                        UserName = entitiesUploadRequest.entityName.ToLower().Replace(' ', '_'),
//                        Email = entitiesUploadRequest.emailAddress,
//                        PhoneNumber = entitiesUploadRequest.phoneNumber,
//                        FirstName = entitiesUploadRequest.entityName,
//                        LastName = entitiesUploadRequest.entityName,
//                        RoleName = role_name
//                    };

//                    //generate random password
//                    var password = _passwdGenService.GenerateRandomPassword();
//                    var result = await _userManager.CreateAsync(user, password);
//                    if (result.Succeeded)
//                    {
//                        var _role = await _roleManager.FindByNameAsync(user.RoleName);
//                        if (_role != null)
//                        {
//                            await _userManager.AddToRoleAsync(user, _role.NormalizedName);
//                        }

//                        foreach (var supplier in entitiesUploadRequest.suppliers)
//                        {
//                            //create a merchant model using identityid from the previous record
//                            Merchant merchant = new Merchant
//                            {
//                                AcceptedTerms = true,
//                                AnchorId = supplier.entityId,
//                                Email = entitiesUploadRequest.emailAddress,
//                                EntityId = entitiesUploadRequest.entityId,
//                                IdentityId = user.Id,
//                                Name = entitiesUploadRequest.entityName,
//                                PhoneNumber = entitiesUploadRequest.phoneNumber,
//                                SupplierId = supplier.entityId
//                            };

//                            EntityRequest er = new EntityRequest { EntityId = entitiesUploadRequest.entityId, AnchorId = supplier.entityId, SupplierId = supplier.entityId };
//                            bool existingEntity = await _registrService.GetEntity(er);
//                            if (!existingEntity)
//                            {
//                                entityList.Add(merchant);
//                            } 
//                        }
                        
//                        var result1 = await _registrService.AddEntities(entityList);

//                        if (result1.Success)
//                        {

//                            reply.ResponseCode = "200";
//                            reply.ReferenceId = Guid.NewGuid().ToString();
//                            reply.ResponseMessage = "Account Creation Processed Successfull. ";

//                            var request = HttpContext.Request;
//                            //var BaseUri = string.Concat(request.Scheme, "://", _appConfig.Value.pa);
//                            //var link = BaseUri;
//                            var link = _appConfig.Value.PayrBaseUrl;
//                            string message =
//                                string.Format("Dear {0}, Your account has been created with username: {1} and password: {2} on the Stanbic Registration Portal. Opt into this service by clicking:  " + link,
//                                    entitiesUploadRequest.entityName, user.UserName, password);
//                            //send sms notification
//                            var entityNotification = new NotificationRequest
//                            {
//                                EmailId = entitiesUploadRequest.emailAddress,
//                                Message = message,
//                                MobileNumber = entitiesUploadRequest.phoneNumber,
//                                ReferenceId = Guid.NewGuid().ToString(),
//                                SendEmail = false,
//                                SendSMS = true,
//                                Subject = $"Entity Creation Notification"
//                            };


//                            var notification = _stanbicService.SendNotification(entityNotification);

//                            if (notification.Success && notification.Data.ResponseCode.Equals("00"))
//                            {
//                                var additionalMessage = "Entity Notification Send Successfull. ";
//                                reply.ResponseMessage += additionalMessage;
//                                //send subsequent notification to stanbic admins
//                                string EmailMessage = $"Account for Entity {entitiesUploadRequest.entityName} has been successfully created on supplychain portal";
//                                List<string> adminAddresses = new List<string>();
//                                adminAddresses.Add("WekesaC@stanbic.com");
//                                adminAddresses.Add("nyanarog@stanbic.com");
//                                adminAddresses.Add("mbuguam@stanbic.com");

//                                var emailNotification = new NotificationRequest
//                                {
//                                    EmailId = $"{adminAddresses[0]},{adminAddresses[1]},{adminAddresses[2]}",
//                                    Message = EmailMessage,
//                                    MobileNumber = entitiesUploadRequest.phoneNumber,
//                                    ReferenceId = Guid.NewGuid().ToString(),
//                                    SendEmail = true,
//                                    SendSMS = false,
//                                    Subject = $"Entity Creation Notification"
//                                };

//                                var adminNotification = _stanbicService.SendNotification(emailNotification);
//                                if (notification.Success && notification.Data.ResponseCode.Equals("00"))
//                                {
//                                    var additionalMessage1 = "Administrators Notification Send Successfull. ";
//                                    reply.ResponseMessage += additionalMessage1;
//                                }
//                                else
//                                {
//                                    var additionalMessage2 = "Administrators Notification Failed. ";
//                                    reply.ResponseMessage += additionalMessage2;
//                                }

//                            }
//                            else
//                            {
//                                var additionalMessage = " Notification Failed. ";
//                                reply.ResponseMessage += additionalMessage;
//                            }

//                        }
//                        else
//                        {
//                            reply.ResponseCode = "500";
//                            reply.ReferenceId = Guid.NewGuid().ToString();
//                            reply.ResponseMessage = "An Error Occured.";
//                        }

//                    }
//                }
//                catch (Exception ex)
//                {
//                    reply.ResponseCode = "500";
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseMessage = "An Error Occured while processing Entity Account. " + ex.Message;
//                }

//            }

//            return Ok(reply);
//        }


//        //feedback loop for loan status update
//        [HttpPost("updateloanstatus")]
//        public async Task<JsonResult> UpdateLoanStatus([FromBody] UpdateLoanStatusRequest loanStatusRequest)
//        {
//            var reply = new UpdateLoanStatusResponse
//            {
//                ReferenceId = "",
//                ResponseCode = "500",
//                ResponseMessage = "Internal Server Error"
//            };

//            if (!ModelState.IsValid)
//            {
//                reply.ReferenceId = Guid.NewGuid().ToString();
//                reply.ResponseCode = "400";
//                reply.ResponseMessage = "Bad Request";
//                return new JsonResult(reply);
//            }


//            var processed = loanStatusRequest.LoanStatus.ToLower() == "processed";
//            var rejected = loanStatusRequest.LoanStatus.ToLower() == "rejected";

//            bool passed = (processed || rejected) ? true : false;

//            if (!passed)
//            {
//                if (!passed)
//                {
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseCode = "405";
//                    reply.ResponseMessage = "Invalid parameters provided for Loan Status";
//                }
//                else
//                {
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseCode = "405";
//                    reply.ResponseMessage = "Invalid parameters provided for Loan Status, Ensure";
//                }

//                return new JsonResult(reply);
//            }


//            //pull loan from db
//            var loanFromDb = await _loanService.FindByReferenceIdAsync(loanStatusRequest.TransactionId);
//            if (loanFromDb.Success && loanFromDb.Data != null)
//            {
//                var loanObject = loanFromDb.Data;
//                //check if loan is already approved
//                if (loanObject.LoanStatus.Equals(LoanStatus.Approved))
//                {
//                    reply.ReferenceId = loanObject.ReferenceId;
//                    reply.ResponseCode = "200";
//                    reply.ResponseMessage = "The Loan Is Already Processed";

//                    return new JsonResult(reply);
//                }

//                var updateResult = await _loanService.UpdateLoanStatusAsync(loanObject, loanStatusRequest);

//                if (updateResult.Success)
//                {
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseCode = "200";
//                    reply.ResponseMessage = "Loan Status Updated Successfully";
//                }
//                else
//                {
//                    reply.ReferenceId = Guid.NewGuid().ToString();
//                    reply.ResponseCode = "400";
//                    reply.ResponseMessage = "An Error Occured While Updating Loan Status";
//                }
//            }
//            else
//            {
//                reply.ReferenceId = Guid.NewGuid().ToString();
//                reply.ResponseCode = "400";
//                reply.ResponseMessage = "An Error Occured While Fetching Loan Details From Database";
//            }

//            return new JsonResult(reply);
//        }



//        #region Methods

//        private string UppercaseFirst(string s)
//        {
//            // Check for empty string.
//            if (string.IsNullOrEmpty(s))
//            {
//                return string.Empty;
//            }
//            // Return char and concat substring.
//            return char.ToUpper(s[0]) + s.Substring(1);
//        }

//        private string GenerateRoleName(string entityType)
//        {
//            string roleName;
//            if (entityType.ToLower().Equals(UserType.Merchant.ToString().ToLower()))
//            {
//                roleName = UserType.Merchant.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Manager.ToString().ToLower()))
//            {
//                roleName = UserType.Manager.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.It.ToString().ToLower()))
//            {
//                roleName = UserType.It.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Anchor.ToString().ToLower()))
//            {
//                roleName = UserType.Anchor.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Aos.ToString().ToLower()))
//            {
//                roleName = UserType.Aos.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Audit.ToString().ToLower()))
//            {
//                roleName = UserType.Audit.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Business.ToString().ToLower()))
//            {
//                roleName = UserType.Business.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Complience.ToString().ToLower()))
//            {
//                roleName = UserType.Complience.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Consumer.ToString().ToLower()))
//            {
//                roleName = UserType.Consumer.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Credit.ToString().ToLower()))
//            {
//                roleName = UserType.Credit.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Enabler.ToString().ToLower()))
//            {
//                roleName = UserType.Enabler.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Supplier.ToString().ToLower()))
//            {
//                roleName = UserType.Supplier.ToString().ToLower();
//            }
//            else if (entityType.ToLower().Equals(UserType.Distributor.ToString().ToLower()))
//            {
//                roleName = UserType.Distributor.ToString().ToLower();
//            }
//            else
//            {
//                roleName = UserType.Consumer.ToString().ToLower();
//            }

//            return UppercaseFirst(roleName);
//        }

//        #endregion
//    }
//}