import { createReducer, on } from '@ngrx/store';
import * as AuthActions from './auth.actions';
import { BaseState } from '../BaseState';

export interface AuthState extends BaseState {
  token: string | null | undefined;
}

export const initialState: AuthState = {
  token: null,
  error: null,
  loading: null
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.login, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(AuthActions.loginSuccess, (state, { loginResponse }) => ({
    ...state,
    token: loginResponse.accessToken,
    loading: false,
    error: null,
  })),
  on(AuthActions.loginFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
