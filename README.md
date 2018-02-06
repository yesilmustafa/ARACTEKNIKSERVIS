Bu uygulamada bir araç teknik servis firmasının iş akışını yönetecek bir web uygulaması yazdık.
Sistemde roller admin,operator ve normal kullanıcı olarak ayrılmış durumda admin girişinde admine özel arıza listeleme ve görüntüleme mevcut
operator girişinde girilen arıza kayıt işleminin kendi sistemine direkt olarak düşmesini sağladık. Normal kullanıcı arıza kaydı oluşturabilirken admin ve operatorler de aynı şekilde arıza kaydı açabiliyor.yalnızca sisteme giriş yapmayan kullanıcılar arıza kaydı oluşturamıyor.
Sistem özellikleri:
• Arıza bildirimi
o Konum girilmeli girilmekte
o Arıza ile ilgili fotoğraf girilmekte
• Üyelik Sistemi
o Email ile doğrulama
• Yönetim kısmı
o 2 tip yönetici
• Admin
• Operatörler
KULLANILAN TEKNOLOJİLER
• NTier Design Pattern
• ASP.Net MVC
• ASP.Net Identity
• EntityFramework CodeFirst
• Repository Design Pattern
• ASP.Net WebApi 2
• ASP.Net Token Based Authentication
