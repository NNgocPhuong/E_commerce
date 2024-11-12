namespace E_Commerce.Helpers
{
    public class MyUtil
    {
        public static string UploadHinh(string folder, IFormFile hinh)
        {
            if (hinh == null || hinh.Length == 0)
            {
                return null;
            }

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string path = Path.Combine(directoryPath, hinh.FileName);
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                hinh.CopyTo(stream);
            }

            return hinh.FileName;
        }

        public static string GenarateRandomKey(int length = 5)
        {
            string result = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += (char)random.Next(65, 90);
            }
            return result;
        }
    }
}
