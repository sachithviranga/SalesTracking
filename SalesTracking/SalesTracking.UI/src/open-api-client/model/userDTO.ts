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
import { UserRoleDTO } from './userRoleDTO';


export interface UserDTO { 
    id?: number;
    isActive?: boolean;
    createDate?: string;
    createBy?: string | null;
    updateDate?: string | null;
    updateBy?: string | null;
    firstName?: string | null;
    lastName?: string | null;
    email?: string | null;
    password?: string | null;
    phoneNumber?: string | null;
    userRole?: Array<UserRoleDTO> | null;
}

