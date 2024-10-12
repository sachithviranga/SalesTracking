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


export interface StockPurchasePaymentDTO { 
    id?: number;
    isActive?: boolean;
    createDate?: string;
    createBy?: string | null;
    updateDate?: string | null;
    updateBy?: string | null;
    stockPurchaseId?: number;
    paymentTypeId?: number;
    paymentTypeName?: string | null;
    chequeNo?: number | null;
    chequeDate?: string | null;
    amount?: number;
    note?: string | null;
    isApproved?: boolean;
    approvedDate?: string | null;
    approvedBy?: string | null;
}

