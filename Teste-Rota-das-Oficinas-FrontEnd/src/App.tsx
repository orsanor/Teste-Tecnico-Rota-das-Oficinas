import { BrowserRouter, Routes, Route } from 'react-router-dom';
import PrivateRoute from './components/ui/PrivateRoute';
import HomePage from './pages/Home';
import Users from './pages/Users';
import Login from './pages/Login';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/HomePage"
          element={
            <PrivateRoute>
              <HomePage />
            </PrivateRoute>
          }
        />
        <Route
          path="/Users"
          element={
            <PrivateRoute>
              <Users />
            </PrivateRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;