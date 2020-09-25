using AutoMapper;
using LicenseGenerator.License;
using LicenseGenerator.Model;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace sistem.manajemen.ppic.bll
{
    public class LoginBLL:ILoginBLL
    {
        private IUnitOfWork _uow;
        private ILoginServices _loginServices;
        private ILicenseEncrypt _encrypt;
        protected const string KeyInput = "BBDk3Cj7UTOm+3izr98Gy7dhsGjtk/lzmFJp6YPVl6SPTzu5rZjNdhP6XcvKeRIlxnq3b+CgxS/LOB64wH/ywhvBOYIlJRc976smvJx7LaqDNkRxxN2bTRG9z9KfWWDBIIAhOP1ULA7XL2fF2hq0dwgWCVBD2+ZfQ212dHdFsMA1/YTYGN8jpU7NI+XT9I43BoUqbcIunTy4JmKrY+w/oKHz19IAM0z4MciUUIbZl5wy4QyxvaNmkllq0KXywD2CkG7fZTGffjRnOt2b4uQ8+h6MrqWWieSae7xCUv7zDH25H/81YsinZvmJPdadtt5WfasXF7mKcG2D5D26enidgnk5NEGWjK456KvC8hrrhrQ5XX8o89KBdb5VHNpelalXw2gywlliyTyF+06nWwiYwYqIQqRnHGnb7p/t+fuY5G/tDlsAy7o8uZ2l+x2QcPIkuHyqnrBV4Nv9GPPNLwAV0gKVd3HNQglKAcWc8eKGZyttcTnHasP5x273QGRcnZ5ZJQh7VfDAeJIw0rXUmv8UxlBBI+5pN5s72kvZ1oC2wYS5w0axzCir0PApMGz/p38xEr8vaFNi12RpoLukLXDyooZD7JMaH6Kzyy/m73iS5BL1rhXZ93JIPKwPmWD1sh0WlOURpNMzjgPusPzuL6WNfY+kS2VvMIrW7ssVq9igBAA=";

        public LoginBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _loginServices = new LoginServices(_uow);
            _encrypt = new LicenseEncrypt();
        }
        public List<LoginDto> GetAll()
        {
            var Data = _loginServices.GetAll().ToList();
            var ReData = Mapper.Map<List<LoginDto>>(Data);
            return ReData;
        }
        public LoginDto GetById(object Id)
        {
            var Data = _loginServices.GetById(Id);
            var ReData = Mapper.Map<LoginDto>(Data);
            return ReData;
        }
        public LoginDto Save(LoginDto Login)
        {
            try
            {
                var serializedPackage = _encrypt.Encrypt(Login.PASSWORD, KeyInput);
                var xmlSerializer = new XmlSerializer(typeof(SerializedPackage));
                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, serializedPackage);
                    var SerializeByte = Encoding.UTF8.GetBytes(textWriter.ToString());
                    var SerializeBas64 = Convert.ToBase64String(SerializeByte);

                    Login.PASSWORD = SerializeBas64;
                }
             
                var db = Mapper.Map<Login>(Login);
                _loginServices.Save(db);
                return Mapper.Map<LoginDto>(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LoginDto VerificationLogin(LoginDto Login)
        {
            var Data = _loginServices.GetById(Login.USER_ID);
            var ReData = Mapper.Map<LoginDto>(Data);
            var decryptResult = "";
            if (Data != null)
            {
                var dataReadByte = Convert.FromBase64String(ReData.PASSWORD);
                var dataString = Encoding.UTF8.GetString(dataReadByte);

                SerializedPackage serializedPackage = null;
                var serializer = new XmlSerializer(typeof(SerializedPackage));

                using (TextReader TextReader = new StringReader(dataString))
                {
                    serializedPackage = (SerializedPackage)serializer.Deserialize(TextReader);
                }

                decryptResult = _encrypt.Decrypt(serializedPackage.KeyBase64, serializedPackage.CipherDataBase64);
            }

            if (ReData != null && Login.USER_ID.ToLower() == ReData.USER_ID.ToLower() && Login.PASSWORD == decryptResult)
            {
                return ReData;
            }
            else
            {
                return null;
            }
        }
        public void ChangePassword(string UserId, string PasswordBaru)
        {
            var GetData = _loginServices.GetById(UserId);

            var serializedPackage = _encrypt.Encrypt(PasswordBaru, KeyInput);
            var xmlSerializer = new XmlSerializer(typeof(SerializedPackage));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, serializedPackage);
                var SerializeByte = Encoding.UTF8.GetBytes(textWriter.ToString());
                var SerializeBas64 = Convert.ToBase64String(SerializeByte);

                GetData.PASSWORD = SerializeBas64;
            }
            GetData.PASSWORD = PasswordBaru;
            _loginServices.Save(GetData);
        }
        public void SetLastOnline(string UserId)
        {
            var GetData = _loginServices.GetById(UserId);
            GetData.LAST_ONLINE = DateTime.Now;
            _loginServices.Save(GetData);
        }
        public DateTime? GetLastOnline(string UserId)
        {
            var GetData = _loginServices.GetById(UserId);

            return GetData == null ? null : GetData.LAST_ONLINE;
        }
    }
}
