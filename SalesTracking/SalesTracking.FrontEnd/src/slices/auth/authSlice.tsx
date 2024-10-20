import { createAsyncThunk, createSlice, SerializedError } from "@reduxjs/toolkit"
import { authApi, LoginDTO, LoginResponse } from "../../api"


export interface AuthState {
  loginResponse: LoginResponse | null,
  loading: boolean
  status: 'idle' | 'loading' | 'succeeded' | 'failed'
  error: SerializedError | null
}

const initialState: AuthState = {
  loginResponse: null,
  loading: false,
  status: 'idle',
  error: null
}

export const login = createAsyncThunk<
  LoginResponse,
  LoginDTO,
  { rejectValue: string }
>
  ('auth/login', async (loginDTO, { rejectWithValue , signal }) => {
    try {
      const { data } = await authApi.apiAuthLoginPost(loginDTO, { signal })
      return data
    } catch (error: any) {
      // if (globalAxios.isAxiosError(error)) {
      //   const customError: SerializedError = {
      //     code: error.response?.status.toString(),
      //     message: t('notification:failed', {
      //       action: t('title.fetch', {
      //         target: t('signDataPage.title')
      //       }),
      //       detail: ''
      //     }),
      //     name: error.name,
      //     stack: error.stack
      //   }
      //   return rejectWithValue(customError)
      // }
      //throw error
      return rejectWithValue('Login failed.'); 
    }
  })

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => { state.loginResponse = null },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.status = 'loading'
        state.error = null
        state.loading = true
      })
      .addCase(login.fulfilled, (state, action) => {
        state.status = 'succeeded'
        state.loginResponse = action.payload
        state.loading = false
      })
      .addCase(login.rejected, (state, action) => {
        state.status = 'failed'
        state.error = action.error
        state.loading = false
      })
  },
});

export const { logout } = authSlice.actions

const authReducer = authSlice.reducer
export default authReducer