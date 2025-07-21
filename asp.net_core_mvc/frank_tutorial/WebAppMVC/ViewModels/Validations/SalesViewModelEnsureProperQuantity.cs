using System.ComponentModel.DataAnnotations;
using CoreBusiness;
using UseCases.ProductsUseCases;

namespace WebAppMVC.ViewModels.Validations
{
    //  Validate the quantity to sell isn't greater than the product quantity
    public class SalesViewModelEnsureProperQuantity : ValidationAttribute
    {
        // Get the SalesViewModel from the validationContext
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // is keyword
            //      Syntax: expression is type
            //
            //      Here, the expression will be evaluated to an instance of some type
            //      And type is the name of the type to that the result of the expression is to be converted
            //      If the expression is not null and the object results from evaluating the expression can be converted to the specified type then is operator will return true otherwise it will return false

            // as keyword
            //      Syntax: expression as type
            //
            //      The above syntax is equivalent to below code. But the expression variable will be evaluated only one time
            //      expression is type ? (type)expression : (type)null
            
            // We are going to apply SalesViewModelEnsureProperQuantity to SalesViewModel Quantity property
            //      Therefore the ObjectInstance is going to be the object instance of SalesViewModel
            var salesViewModel = validationContext.ObjectInstance as SalesViewModel;

            if (salesViewModel != null)
            {
                // Currently we already have two ways to validate
                //      min attribute of input element
                //      Range attribute
                //      It doesn't hurt to add another extra layer of safety
                if (salesViewModel.QuantityToSell <= 0)
                {
                    // Means validation failed
                    return new ValidationResult("The quantity to sell has to be greater than zero.");
                }
                else
                {
                    // var product = ProductsRepository.GetProductById(salesViewModel.SelectedProductId);
                    var viewSelectedProductUseCase = validationContext.GetService(typeof(IViewSelectedProductUseCase)) as IViewSelectedProductUseCase;

                    if (viewSelectedProductUseCase != null)
                    {
                        var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);

                        if (product != null)
                        {
                            if (product.Quantity < salesViewModel.QuantityToSell)
                            {
                                return new ValidationResult($"{product.Name} only has {product.Quantity} left. It is not enough.");
                            }
                        }
                        else
                        {
                            return new ValidationResult("The selected product doesn't exist.");
                        }
                    }
                    else
                    {
                        return new ValidationResult("The viewSelectedProductUseCase class doesn't exist.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}