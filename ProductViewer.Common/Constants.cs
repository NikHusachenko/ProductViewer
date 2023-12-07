namespace ProductViewer.Common
{
    public static class DbTables
    {
        public const string PRODUCT_TABLE_NAME = "Products";
    }

    public static class Errors
    {
        public const string PRODUCT_ALREADY_EXISTS_ERROR = "Product already exists";
        public const string PRODUCT_NOT_FOUND_ERROR = "Product not found";
        public const string CANT_CREATE_PRODUCT_ERROR = "Can't create product";
        public const string CANT_SAVE_PRODUCT_IMAGE_ERROR = "Can't save product image error";
        public const string CANT_REMOVE_PRODUCT_IMAGE_ERROR = "Can't remove product image error";
        public const string CANT_DELETE_PRODUCT_ERROR = "Can't delete product";
        public const string CANT_UPDATE_PRODUCT_ERROR = "Can't update product";
        public const string PRODUCT_IMAGE_NOT_FOUND_ERROR = "Product image not found";
    }

    public static class Cofigurations
    {
        public const string IMAGE_DIRECTORY_NAME = "images\\product";
    }

    public static class DataPagination
    {
        public const int PRODUCTS_ON_PAGE = 20;

        public static int CalSkipRecords(int page, int pageSize) => (page - 1) * pageSize;
    }
}