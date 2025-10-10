import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export function firstLetterShouldBeUppercase(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const value = <string>control.value;   
        
        if (!value) return null;
        
        const firstLetter = value[0];
        
        const validationError = {
            firstLetterShouldBeUppercase: {
                message: ""
            }
        };
        
        if (!firstLetter.match(/[a-z]/i)) {
            validationError.firstLetterShouldBeUppercase.message = "The first letter should be an English letter";
            return validationError;
        }
        
        if (firstLetter !== firstLetter.toUpperCase()) {
            validationError.firstLetterShouldBeUppercase.message = "The first letter should be uppercase";
            return validationError;
        }
        
        return null;
    };
}