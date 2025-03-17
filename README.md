# nestpay for .net 4.6.1
                nestpay = new Nestpay.Nestpay(Nestpay.MODE.Prod, Nestpay.BANK.Halkbank);

                //string orderId = **guid;
                //string ipAddress = GetIpAddress(System.Web.HttpContext.Current.Request.ServerVariables);

                nestpay.SetClientId(""); // İşyeri numarası
                nestpay.SetUsername(""); // Kullanıcı adı
                nestpay.SetPassword(""); // Kullanıcı şifresi

                var request = new Nestpay.Nestpay.CC5Request();
                request.SetCardNumber(""); // Kart numarası
                request.SetCardExpiry("", ""); // Son kullanma tarihi - AA,YY
                request.SetCardCode(""); // Kart arkasındaki 3 haneli numara
                request.SetAmount("19.00", "TRY"); // Satış tutarı ve para birimi
                request.SetInstallment(""); // Taksit sayısı (varsa)
                request.SetIPv4((string.IsNullOrWhiteSpace(ipAddress) || ipAddress.Length < 7) ? "127.0.0.1" : ipAddress); // Müşteri IP adresi
                request.SetOkUrl("");
                request.SetFailUrl("");
                //request.SetOrderId(orderId); // Sipariş numarası

                var response = nestpay.Auth(request);
                if (!string.IsNullOrEmpty(response.ErrMsg))
                {
                    return response.ErrMsg;
                }
                else
                {
                    //success
                }