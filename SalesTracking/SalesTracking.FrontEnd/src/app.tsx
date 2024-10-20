
import './App.css';
import { Router } from './routes/routes';
import { ThemeProvider } from './theme/theme-provider';

function App() {
  return (
    <ThemeProvider>
      <Router />
    </ThemeProvider>
  )
}

export default App;
