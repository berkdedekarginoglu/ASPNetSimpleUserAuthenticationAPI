using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Constants
{
    public static class Messages
    {
        public static string
            UserAdded = "Kullanıcı eklendi",
            UserNotFound = "Kullanıcı bulunamadı",
            UserDeleted = "Kullanıcı silindi",
            UserUpdated = "Kullanıcı güncellendi",
            UserLoggedIn = "Kullanıcı girişi başarılı",
            UserPasswordNotMatch = "Eposta veya parola hatalı",
            UserAlreadyExist = "Kullanıcı mevcut",
            CustomerAdded = "Müşteri eklendi",
            OperationClaimAdded = "Yetki eklendi",
            OperationClaimDeleted = "Yetki silindi",
            OperationClaimNotFound = "Yetki bulunamadı",
            OperationClaimUpdated = "Yetki güncellendi",
            UserOperationClaimNotFound = "Yetki ilişkisi bulunamadı",
            UserOperationClaimAdded = "Yetki ilişkisi eklendi",
            PasswordModified = "Parola değiştirildi",
            OldPasswordNotVerified = "Eski parola uyuşmuyor",
            LoginAuthenticationFailed = "Giriş kodu doğrulanmadı",
            LoginAuthenticationCodeSent = "Epostanıza oturum kodu gönderildi",
            LoginSessionNotValid = "Geçerli bir oturum bulunamadı",
            ForgotPasswordResetCodeSent = "Epostanıza sıfırlama kodu gönderildi";
    }
}
