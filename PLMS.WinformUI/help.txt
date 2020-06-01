//cmd 运行前先将App.config 重命名为Web.config
cd C:\Windows\Microsoft.NET\Framework64\v2.0.50727

//加密
aspnet_regiis -pef "appSettings" "C:\Users\92316\Desktop\PLMS\PLMS.WinformUI" -prov "DataProtectionConfigurationProvider"

//解密
aspnet_regiis -pdf "appSettings" "C:\Users\92316\Desktop\PLMS\PLMS.WinformUI"