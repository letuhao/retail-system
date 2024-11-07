namespace RS.CommonLibrary.Constants
{
    public static class CommonConstants
    {
        public const int PERFORMANCE_LIMIT_TIME = 3;
        public const int PAGING_DEFAULT_FISRT_PAGE = 1;
        public const int PAGING_DEFAULT_PAGE_SIZE = 20;

        public enum USER_ROLE
        {
            GUEST = 0,
            USER = 1,
            SHOP_OWNER = 2,
            ORDER_MANAGEMENT = 3,
            ADMIN = 4
        }
    }
}
