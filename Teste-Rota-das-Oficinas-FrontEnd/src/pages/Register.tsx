import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";
import { RegisterForm } from "@/components/ui/Register";

const Register = () => {
  const [form, setForm] = useState({
    userName: "",
    name: "",
    email: "",
    password: "",
    passwordConfirmation: "",
  });

  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (form.password !== form.passwordConfirmation) {
      setMessage("As senhas não coincidem.");
      return;
    }

    try {
      await api.post("/user", form);
      alert("Usuário registrado com sucesso!");
      navigate("/");
    } catch (error) {
      setMessage("Erro ao registrar usuário.");
      console.error(error);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-gray-100 p-4 flex items-center justify-center" >
      <div className="w-full max-w-md">
        <RegisterForm
          form={form}
          onChange={handleChange}
          onSubmit={handleSubmit}
          error={message}
          onBackClick={() => navigate("/")}
        />
      </div>
    </div>
  
  );
};

export default Register;