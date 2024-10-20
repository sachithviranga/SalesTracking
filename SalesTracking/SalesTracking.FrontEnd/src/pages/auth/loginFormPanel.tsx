import { FormProvider, useForm } from "react-hook-form"
import LoginInputPanel from "../../features/auth/loginInputPanel"
import { login } from "../../slices/auth/authSlice"
import { useAppDispatch } from "../../hooks/useRedux"
import LoginButtonPanel from "../../features/auth/loginButtonPanel"


function LoginFormPanel() {

    const methods = useForm({})

    const dispatch = useAppDispatch()

    const { getValues, handleSubmit } = methods

    const submit = () => {
        const values = getValues()
        dispatch(login({ userName: values.username, passWord: values.password }))
    }

    return (
        <>
            <FormProvider {...methods}>
                <form onSubmit={handleSubmit(submit)}>
                    <LoginInputPanel />

                    <LoginButtonPanel submitClick={submit} />
                </form>
            </FormProvider>
        </>
    )
}

export default LoginFormPanel