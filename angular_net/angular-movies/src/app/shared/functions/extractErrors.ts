export function extractErrors(obj: any): string[] {
    const err = obj.error.errors;
    const errorMessages: string[] = [];
    
    for (const key in err) {
        const messagesWithField: string[] = err[key].map((errorMessage: string) => `${key}: ${errorMessage}`);
        errorMessages.push(...messagesWithField);
    } 
    
    return errorMessages;
}

export function extractIdentityErrors(obj: any): string[] {
    const err = obj.error;
    const errorMessages: string[] = [];
    
    for (let i = 0; i < err.length; i++) {
        errorMessages.push(err[i].description);
    }
    
    return errorMessages;
}