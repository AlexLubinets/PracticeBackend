namespace ProductApp.BLL.Constants
{
    public static class ErrorMessages
    {
        public const string RegFail = "Registration failed.";
        public const string LoginFail = "Username or password is incorrect.";
        public const string ProductNotFound = "This product doesn't exist.";
        public const string InvalidPrice = "Price of the product must be less than 10.000 and greater than 0";
        public const string InvalidProductName = "Name of the product must contain less than 50 characters";
        public const string ProductAlreadyExists = "Product with this name already exists.";
        public const string InvalidProductAmount = "Amount of products must be:\n - less than or equal to 1.000;\n - greater than 0.";
        public const string AmountNotEnough = "Amount of product in stock is not enough.";
        public const string InvalidOperation = "This operation cannot be executed.";

    }
}
