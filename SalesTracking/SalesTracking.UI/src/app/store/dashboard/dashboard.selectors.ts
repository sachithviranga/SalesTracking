import { createSelector, createFeatureSelector } from '@ngrx/store';
import { DashboardState } from './dashboard.reducer';

export const selectAvailableProductState = createFeatureSelector<DashboardState>('dashboard');

export const selectAvailableProducts = createSelector(
    selectAvailableProductState,
    (state: DashboardState) => state.products
);

export const selectProductError = createSelector(
    selectAvailableProductState,
    (state: DashboardState) => state.error
);
