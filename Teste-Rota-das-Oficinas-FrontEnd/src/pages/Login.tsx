import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";
import { LoginForm } from "../components/ui/LoginForm";

const Login = () => {
  const [form, setForm] = useState({ userName: "", password: "" });
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      interface LoginResponse {
        accessToken: string;
      }

      const response = await api.post<LoginResponse>("/auth/login", form);
      localStorage.setItem("token", response.data.accessToken);
      navigate("/HomePage");
    } catch (error) {
      console.error(error);
      setMessage("Credenciais inválidas. Tente novamente.");
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-gray-100 p-4 flex items-center justify-center" >
      <div className="w-full max-w-md">
        <LoginForm
          form={form}
          onChange={handleChange}
          onSubmit={handleSubmit}
          error={message}
          onRegisterClick={() => navigate("/Register")}
        />
      </div>
    </div>
  );
};

export default Login;