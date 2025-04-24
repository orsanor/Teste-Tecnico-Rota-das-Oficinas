import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";
import { RegisterForm } from "@/components/ui/Register";

export default function Register() {
  const [form, setForm] = useState({
    userName: "",
    name: "",
    email: "",
    password: "",
    passwordConfirmation: "",
  });

  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const errorMessagesMap = {
    "Passwords must have at least one non alphanumeric character.": "A senha deve conter pelo menos um caractere não alfanumérico.",
    "Passwords must have at least one digit ('0'-'9').": "A senha deve conter pelo menos um dígito ('0'-'9').",
    "Passwords must have at least one uppercase ('A'-'Z').": "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z').",
  };

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
      const errorMessages = error?.response?.data?.errors || [];
      const translatedErrors = errorMessages.map((msg: string) => errorMessagesMap[msg] || msg).join(" ");
      const errorMessage = translatedErrors;
      setMessage(errorMessage);
      console.error("Erro ao registrar:", error);
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