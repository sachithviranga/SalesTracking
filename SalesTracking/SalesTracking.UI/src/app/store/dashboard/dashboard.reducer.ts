import { createReducer, on } from '@ngrx/store';
import * as Actions from './dashboard.action'
import { BaseState } from '../BaseState';
import { ProductQtyDTO } from 'src/open-api-client';

export interface DashboardState extends BaseState {
    products: Array<ProductQtyDTO> | null;
}

export const initialState: DashboardState = {
    products: null,
    error: null,
    loading: null
};

export const dashboardReducer = createReducer(
    initialState,
    on(Actions.loadAvailableProdcuts, (state) => ({
        ...state,
        loading: true,
        error: null,
    })),
    on(Actions.loadAvailableProdcutsSuccess, (state, { prodcuts }) => ({
        ...state,
        products: prodcuts,
        loading: false,
        error: null,
    })),
    on(Actions.loadAvailableProdcutsFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error,
    }))
);
