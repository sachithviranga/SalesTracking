import { lazy } from 'react'
import { Navigate, useRoutes } from 'react-router-dom'


export const LoginPage = lazy(() => import('../pages/auth/loginPage'))
export const DashboardPage = lazy(() => import('../pages/dashboard/dashboardPage'))

export function Router() {
    return useRoutes([
        {
            path: 'login',
            element: <LoginPage />,
        },
        {
            path: '/',
            element: <Navigate to="/login" replace />,
        },
        {
            path: '*',
            element: <Navigate to="/login" replace />,
        },
        {
            path: 'dashboard',
            element: <DashboardPage />,
        }
    ])
}