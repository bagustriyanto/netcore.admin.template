using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SarayaAdmin.Common;
using SarayaAdmin.Common.Constant;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Cores;
using SarayaAdmin.Service.Responses;
using Threenine.Data;

namespace SarayaAdmin.Service.Services {
    public class UserServices : IUserServices {
        private static IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _accessor;

        public UserServices (IUnitOfWork unitOfWork, IHttpContextAccessor accessor) {
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }
        public BaseResponse<User> Create (User model) {
            BaseResponse<User> result = new BaseResponse<User> ();
            Expression<Func<Credentials, bool>> predicateCredential = null;

            try {
                predicateCredential = x => x.Email.Equals (model.Credential.Email);
                var credentialModel = _unitOfWork.GetRepository<Credentials> ().Single (predicateCredential);
                if (credentialModel != null)
                    throw new Exception ("ERROR-0001");

                predicateCredential = x => x.Username.Equals (model.Credential.Username);
                credentialModel = _unitOfWork.GetRepository<Credentials> ().Single (predicateCredential);
                if (credentialModel != null)
                    throw new Exception ("ERROR-0002");

                var password = new Password ();
                model.Credential.Salt = password.Salt ();
                model.Credential.Password = new Password ().PasswordEncrypt (model.Credential.Password, model.Credential.Salt);
                model.Credential.Status = true;
                model.Credential.CreatedBy = model.Credential.Username;
                model.Credential.CreatedOn = DateTime.Now;
                model.Credential.CreatedHost = _accessor.HttpContext.Connection.RemoteIpAddress.ToString ();

                model.CreatedBy = model.Credential.Username;
                model.CreatedOn = DateTime.Now;
                model.CreatedHost = _accessor.HttpContext.Connection.RemoteIpAddress.ToString ();

                _unitOfWork.GetRepository<User> ().Add (model);
                _unitOfWork.SaveChanges ();

                result.Status = true;
                result.Message = "INFO-0005";
            } catch (Exception ex) {
                result.Message = ex.Message.Contains ("ERROR-") == false ? "ERROR-0000" : ex.Message;
            }

            return result;
        }

        public BaseResponse<User> Update (User model) {
            BaseResponse<User> result = new BaseResponse<User> ();
            Expression<Func<User, bool>> predicate = null;
            Expression<Func<Credentials, bool>> predicateCredential = null;

            try {
                predicateCredential = x => x.Id.Equals (model.Credential.Id);
                var credentialModel = _unitOfWork.GetRepository<Credentials> ().Single (predicateCredential);
                if (credentialModel == null)
                    throw new Exception ("ERROR-0003");

                credentialModel.Username = model.Credential.Username;
                credentialModel.Status = model.Credential.Status;
                credentialModel.ModifiedBy = model.Credential.Username;
                credentialModel.ModifiedOn = DateTime.Now;
                credentialModel.ModifiedHost = _accessor.HttpContext.Connection.RemoteIpAddress.ToString ();

                _unitOfWork.GetRepository<Credentials> ().Update (credentialModel);

                predicate = x => x.Credential.Equals (credentialModel.Id);
                var userModel = _unitOfWork.GetRepository<User> ().Single (predicate);
                if (userModel == null)
                    throw new Exception ("ERROR-0003");

                userModel.FirstName = model.FirstName;
                userModel.LastName = model.LastName;
                userModel.Phone = model.Phone;
                userModel.ModifiedBy = model.Credential.Username;
                userModel.ModifiedOn = DateTime.Now;
                userModel.ModifiedHost = _accessor.HttpContext.Connection.RemoteIpAddress.ToString ();

                _unitOfWork.GetRepository<User> ().Update (userModel);
                _unitOfWork.SaveChanges ();

                result.Status = true;
                result.Message = "INFO-0006";
            } catch (Exception ex) {
                result.Message = ex.Message.Contains ("ERROR-") == false ? "ERROR-0000" : ex.Message;
            }

            return result;
        }

        public BaseResponse<User> Delete (User model) {
            BaseResponse<User> result = new BaseResponse<User> ();
            Expression<Func<User, bool>> predicate = null;
            Expression<Func<Credentials, bool>> predicateCredential = null;

            try {
                predicateCredential = x => x.Id.Equals (model.Credential.Id);
                var credentialModel = _unitOfWork.GetRepository<Credentials> ().Single (predicateCredential);

                _unitOfWork.GetRepository<Credentials> ().Delete (credentialModel);

                predicate = x => x.CredentialId.Equals (model.Credential.Id);
                var userModel = _unitOfWork.GetRepository<User> ().Single (predicate);

                _unitOfWork.GetRepository<User> ().Delete (userModel);
                _unitOfWork.SaveChanges ();

                result.Status = true;
                result.Message = "INFO-0007";
            } catch (Exception ex) {
                result.Message = ex.Message.Contains ("ERROR-") == false ? "ERROR-0000" : ex.Message;
            }

            return result;
        }

        public BaseResponse<User> Get (User model) {
            BaseResponse<User> result = new BaseResponse<User> ();

            return result;
        }

        public BaseResponse<User> GetAll (string term, int limit, int index) {
            BaseResponse<User> result = new BaseResponse<User> ();
            Expression<Func<User, bool>> predicate = null;

            try {
                if (!string.IsNullOrEmpty (term)) {
                    predicate = prop => prop.FirstName.ToLower ().Contains (term) ||
                        prop.LastName.ToLower ().Contains (term) || prop.Credential.Username.ToLower ().Contains (term) ||
                        prop.Credential.Email.ToLower ().Contains (term);
                }

                result.ListData = _unitOfWork.GetReadOnlyRepository<User> ().GetList (predicate: predicate, orderBy: src => src.OrderBy (x => x.Id),
                    include: src => src.Include (x => x.Credential), size: limit, index: index);
                result.Status = true;
                result.Message = Message.INFO9999;
            } catch (Exception ex) {
                result.Message = ex.Message.Contains ("ERROR-") == false ? Message.ERR0000 : ex.Message;
            }

            return result;
        }
    }
}