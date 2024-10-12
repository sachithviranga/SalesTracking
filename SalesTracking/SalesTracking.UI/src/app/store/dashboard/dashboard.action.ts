import { createAction, props } from '@ngrx/store';
import { ProductQtyDTO } from 'src/open-api-client';

export const loadAvailableProdcuts = createAction(
  '[Dashboard] Load available products'
);

export const loadAvailableProdcutsSuccess = createAction(
  '[Dashboard] Load available products Success',
  props<{ prodcuts : Array<ProductQtyDTO> }>()
); 

export const loadAvailableProdcutsFailure = createAction(
  '[Dashboard] Load available products Failure',
  props<{ error: any }>()
);
