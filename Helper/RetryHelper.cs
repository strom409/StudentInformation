namespace StudentInformation.Helper
{
    public class RetryHelper
    {

        public static void RetryOnException(int time, TimeSpan delay, Action operation)
        {
            int num = 0;
            while (true)
            {
                try
                {
                    num++;
                    operation();
                    break;
                }
                catch (Exception)
                {
                    if (num == time)
                    {
                        throw;
                    }

                    Task.Delay(delay).Wait();
                }
            }
        }
    }
}
