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


export interface PaymentsDTO { 
    id?: number;
    isActive?: boolean;
    createDate?: string;
    createBy?: string | null;
    updateDate?: string | null;
    updateBy?: string | null;
    invoiceNo?: string | null;
    paymentTypeId?: number;
    paymentTypeName?: string | null;
    chequeNo?: number | null;
    chequeDate?: string | null;
    amount?: number;
    salesId?: number;
    transactionDate?: string;
}

