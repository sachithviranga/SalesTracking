import { createAction, props } from '@ngrx/store';
import { LoginDTO, LoginResponse } from 'src/open-api-client';

export const login = createAction(
  '[Auth] Login',
  props<{ loginDTO: LoginDTO  }>()
);

export const loginSuccess = createAction(
  '[Auth] Login Success',
  props<{ loginResponse: LoginResponse }>()
); 

export const loginFailure = createAction(
  '[Auth] Login Failure',
  props<{ error: any }>()
);
