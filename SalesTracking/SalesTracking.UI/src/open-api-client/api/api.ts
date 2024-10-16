export * from './auth.service';
import { AuthService } from './auth.service';
export * from './customerData.service';
import { CustomerDataService } from './customerData.service';
export * from './dashboard.service';
import { DashboardService } from './dashboard.service';
export * from './masterData.service';
import { MasterDataService } from './masterData.service';
export * from './payments.service';
import { PaymentsService } from './payments.service';
export * from './productData.service';
import { ProductDataService } from './productData.service';
export * from './roleData.service';
import { RoleDataService } from './roleData.service';
export * from './sales.service';
import { SalesService } from './sales.service';
export * from './stock.service';
import { StockService } from './stock.service';
export * from './userData.service';
import { UserDataService } from './userData.service';
export * from './weatherForecast.service';
import { WeatherForecastService } from './weatherForecast.service';
export const APIS = [AuthService, CustomerDataService, DashboardService, MasterDataService, PaymentsService, ProductDataService, RoleDataService, SalesService, StockService, UserDataService, WeatherForecastService];
