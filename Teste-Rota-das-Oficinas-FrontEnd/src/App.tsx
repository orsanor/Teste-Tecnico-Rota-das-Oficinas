import { BrowserRouter, Routes, Route } from "react-router-dom"
// import Produtos from "./pages/Products"
import Login from "./pages/Login"
import Register from "./pages/Register"

function App() {
  return (
    <div className="min-h-screen bg-background text-foreground">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Register />} />
          {/* <Route path="/produtos" element={<Produtos />} />  */}
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App