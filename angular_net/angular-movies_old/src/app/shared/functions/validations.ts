import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

// Typescript use the keyword "export" to make classes and interfaces public
//      The primary reason is that export matches the plans for ECMAScript
//          You could argue that "they should have used "export" instead of "public"
//          But asides from "export/private/protected" being a poorly matched set of access modifiers, I believe there is a subtle difference between the two that explains this
//
//      In TypeScript, marking a class member as public or private has no effect on the generated JavaScript
//          It is simply a design / compile time tool that you can use to stop your TypeScript code accessing things it shouldn't
//
//      With the export keyword, the JavaScript adds a line to add the exported item to the module
//          In your example: here.SomeClass = SomeClass;
//
//      So conceptually, visibility as controlled by public and private is just for tooling, whereas the export keyword changes the output
export function firstLetterShouldBeUppercase(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        const value = <string>control.value;

        if (!value) return null;
        if (value.length === 0) return null;

        const firstLetter = value[0];

        if (firstLetter !== firstLetter.toUpperCase()) {
            return {
                firstLetterShouldBeUppercase: {
                    message: 'The first letter should be uppercase'
                }
            };
        }

        return null;
    }
}