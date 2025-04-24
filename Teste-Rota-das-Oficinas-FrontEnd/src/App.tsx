import { BrowserRouter, Routes, Route } from "react-router-dom"
import HomePage from "./pages/Home"
import Login from "./pages/Login"
import Register from "./pages/Register"
import ProfilePage from "./pages/Users"

function App() {
  return (
    <div className="min-h-screen bg-background text-foreground">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/Register" element={<Register />} />
          <Route path="/Home" element={<HomePage/>} />
          <Route path="/Profile" element={<ProfilePage />} />
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App