/**
 * SalesTracking API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


export interface PaymentDTO { 
    id?: number;
    isActive?: boolean;
    createDate?: string;
    createBy?: string | null;
    updateDate?: string | null;
    updateBy?: string | null;
    invoiceNo?: string | null;
    paymentTypeId?: number;
    chequeNo?: number | null;
    salesId?: number;
    transactionDate?: string;
}

