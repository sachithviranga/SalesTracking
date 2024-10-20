
import { useState } from "react"

import { Box, TextField, IconButton, InputAdornment } from '@mui/material'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { useFormContext } from "react-hook-form"


function LoginInputPanel() {

    const { register, formState: { errors } } = useFormContext()

    const [showPassword, setShowPassword] = useState(false)

    const handleClickShowPassword = () => {
        setShowPassword((prev) => !prev)
    }

    const handleMouseDownPassword = (event: any) => {
        event.preventDefault()
    }

    return (
        <>
            <Box sx={{ display: 'flex', flexDirection: 'column', width: '300px', m: 5 }}>
                <TextField
                    fullWidth
                    label="Email address"
                    sx={{ mb: 3 }}
                    {...register('username', {
                        required: 'User Name is required',
                        pattern: {
                          value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/,
                          message: 'Invalid email address',
                        },
                      })}
                      error={!!errors.username}
                      variant="outlined"
                      helperText = {errors.username ? errors.username.message?.toString() : ''}
                />

                <TextField
                    label="Password"
                    variant="outlined"
                    type={showPassword ? 'text' : 'password'}
                    fullWidth
                    {...register('password', { required: 'Password is required', minLength: { value: 6, message: 'Password must be at least 6 characters' } })}
                    error={!!errors.password}
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position="end">
                                <IconButton
                                    onClick={handleClickShowPassword}
                                    onMouseDown={handleMouseDownPassword}
                                    edge="end"
                                    aria-label="toggle password visibility"
                                >
                                    {showPassword ? <VisibilityOff /> : <Visibility />}
                                </IconButton>
                            </InputAdornment>
                        ),
                    }}
                    helperText = {errors.password ? errors.password.message?.toString() : ''}
                    sx={{ mb: 3 }}
                />
            </Box>
        </>
    )
}

export default LoginInputPanel