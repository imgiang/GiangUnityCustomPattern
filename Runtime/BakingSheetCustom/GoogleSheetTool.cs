using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Internal;
using Cathei.BakingSheet.Unity;
using GiangCustom.Runtime.BakingSheetCustom.Containers;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class GoogleSheetTools
    {
        // unit test google account credential
        private static readonly string GoogleCredential = @"{
  ""type"": ""service_account"",
  ""project_id"": ""princess-dress-up---anime-doll"",
  ""private_key_id"": ""5016303de61b351332c3e26ed4e53b75267fc8f9"",
  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC5dOHq87g5Hxzy\nR9ial25pIYVqj3sUYFAmN5TyCSRAMKkxW9nnWiAxCWBUFbMDBwn2hivA3WdfOcEn\nN/tgvauXX8aF+Yo3J+HSINyAQdbfVKXX9t9X9OhdHeArZTWShXXEYIiFWjFtEzdd\nCqd0b4+7UYZhjRwj7Xm8IyOhokAoiyBtaCD06BYf5FUoOY5dCaZgJYjvDcmlyX1g\nBMHcQdvJahxBtHqCFjwXGfNgNEQ1+hfI6zRJMTzmQJDgWJ+ZSXl5FAdLdru0+oX4\nI6yRxkj5FoB8vN+pzJjySyVrursNIHaEppycI337epqZGPYpGIsIL0g1TPTGCWB2\nA/kx3hurAgMBAAECggEARHEINuc+44XB7v/oxhSAoLfIPu04pOpuacw1YaVSBYZv\nylF1d1Xhb3dgX2eqqsFcQeh+GS0cSx5LVCfq02br/m9QGn1fLr+/LQcHmSWzILRi\nh/MJtbIiVssBwcIWJT6vmYtulpgUiYZ/9gxKhEPvrVeG2lVzPycSuAXXznzMZKlh\nlgvUli1OcR/QEoyOl1S7Hx1oVqBFsB6Jqyqmz8gCZWUV4S+x5qDAlMTheU3/wOn5\niwaNPB2kDO+JZBUgy9dyBvbJ330NYxSVt3HNX1WHfNg1b/ExsSxI51GoWPt4YLIJ\ne78hUgTpGHlVPJPbGyljcHdrpm6ZaMJ6P4yX8qNJIQKBgQD1YEhvIE1i5nGUFn2Q\nISv81NQ6m2RnD2PWEtj638Q76p9je2qCmNIVhrihe6WifXFS6lL1gKJ33XFoBDoX\nS33dTf0AH/Hv2rghFfN47zzWqjPFaZW5f+j/2TMGVQmMidnFjs9vLOjgxkyLwFxi\nIxgQShEAGHsVwFYdPFHGnqG9swKBgQDBfHWM7q+b2VZKPgjz10bTkRzFBcYArOs2\nc4FSkmceo2AOuAvo5hV7E6gQpas4X3TzIowMiquvZLAvfWcHI9Bqtmre4cbAH5bJ\nm0woXF9VBu89IyEI96Ipuw3r35cmd3r/phbivLS/vcfbFrVtJVksHxtWgl28/SYo\novDXvvVeKQKBgQC62yZt0Yh83wXxAqt9vXUU5TH0q27q+JJLbDDl4s853XKhPFOg\nviWFlQE2n8VDlwlcXers35dZdj2tO0LNiISBqakXljwULlf3ghLVrkGDKzufgscs\n6tYRN2Ke2NUbu8IlqpoWjHmO6hNSfRsc9KKXjP+jNgFNUBmDy/JuMVDz9wKBgFNX\nTbbv1Np3ijCeefK2Rr64ocDH1NrToNkqdYjgoORUkPqmEhM2kAgclmbdRVwZ2eEk\nf1ijnoIFB9Lc3DT6Gzrr6iqo65gzwxqB9xLnfvS78O7Po9od+E6rULrJ62xZSLS3\nkOIHUH/KujxHksw5qyhTGc5whvnxvK8CSGVVzpDJAoGAcGGKaC3KADmX62U3BKNG\nShw2VgjN5KRQv6T011oXU+MYeJML8wxTzhK8/aJEqccX2kA1od2Ts63+nq50PiJU\nGN2/x6zXEdC/H25SrunRXbDDMrxRzS/3qVH9B59G4poqQuGw9H7rItR36VhNi9we\nPLi+x941sBbpev/8pRxLHOo=\n-----END PRIVATE KEY-----\n"",
  ""client_email"": ""rotateringggsheet@princess-dress-up---anime-doll.iam.gserviceaccount.com"",
  ""client_id"": ""112468562986835801963"",
  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
  ""token_uri"": ""https://oauth2.googleapis.com/token"",
  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/rotateringggsheet%40princess-dress-up---anime-doll.iam.gserviceaccount.com"",
  ""universe_domain"": ""googleapis.com""
}";

        public class PrettyJsonConverter : JsonSheetConverter
        {
            public PrettyJsonConverter(string path, IFileSystem fileSystem = null) : base(path, fileSystem)
            { }

            public override JsonSerializerSettings GetSettings(Microsoft.Extensions.Logging.ILogger logError)
            {
                var settings = base.GetSettings(logError);

                settings.Formatting = Formatting.Indented;

                return settings;
            }
        }

        [MenuItem("BakingSheet/Sample/Import From Google/ Import Rules")]
        public static async void ConvertFromGoogle(string id)
        {
            var jsonPath = Path.Combine(Application.streamingAssetsPath, "Google");

            var googleConverter = new GoogleSheetConverter(id, GoogleCredential, TimeZoneInfo.Utc);

            var sheetContainer = new SheetContainer();

            await sheetContainer.Bake(googleConverter);

            // create json converter to path
            var jsonConverter = new PrettyJsonConverter(jsonPath);

            // save datasheet to streaming assets
            await sheetContainer.Store(jsonConverter);

            AssetDatabase.Refresh();

            Debug.Log("Google sheet converted.");
        }
        
    }
