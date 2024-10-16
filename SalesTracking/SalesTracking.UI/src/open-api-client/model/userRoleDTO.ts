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
import { RoleDTO } from './roleDTO';


export interface UserRoleDTO { 
    id?: number;
    isActive?: boolean;
    createDate?: string;
    createBy?: string | null;
    updateDate?: string | null;
    updateBy?: string | null;
    userId?: number;
    roleId?: number;
    role?: RoleDTO;
}

