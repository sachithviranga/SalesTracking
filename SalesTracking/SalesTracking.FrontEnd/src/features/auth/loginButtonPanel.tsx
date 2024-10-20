import { Box, Button } from "@mui/material"


export interface LoginButtonPanelProps {
    submitClick: () => void
}


function LoginButtonPanel({ submitClick }: LoginButtonPanelProps) {

    return (
        <>
            <Box sx={{ display: 'flex', flexDirection: 'column', width: '300px', m: 5 }}>

                <Button type="submit" variant="outlined" onClick={submitClick}>Login</Button>

            </Box>
        </>)

}

export default LoginButtonPanel