import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ArrowBigLeft } from "lucide-react";
import api from "../../services/api";
import { useNavigate } from "react-router-dom";

interface UserData {
  name: string;
  userName: string;
  id?: string;
}

export default function Profile() {
  const navigate = useNavigate();
  const [user, setUser] = useState<UserData>({
    name: "",
    userName: "",
  });

  const [editUser, setEditUser] = useState({
    name: "",
    userName: "",
    password: "",
  });

  const updateUserProfile = async () => {
    try {
      if (!user.id) {
        throw new Error("ID do usuário não encontrado");
      }

      await api.put(`/user/${user.id}`, editUser);
      setUser({ name: editUser.name, userName: editUser.userName });
      alert("Perfil atualizado com sucesso!");
    } catch (error) {
      console.error("Erro ao atualizar o perfil", error);
      alert("Erro ao atualizar o perfil");
    }
  };

  const goToHomePage = () => {
    navigate("/HomePage");
  };

  return (
    <main className="min-h-screen bg-gray-50">
      <nav className="bg-white shadow p-4 mb-6">
        <div className="max-w-7xl mx-auto flex items-center justify-between">
          <h1 className="text-2xl font-bold text-gray-800">
            Perfil de Usuário
          </h1>
          <button
            onClick={goToHomePage}
            className="text-gray-800 hover:text-blue-600 transition-colors duration-200 cursor-pointer"
          >
            <div className="flex items-center justify-center w-10 h-10 bg-gray-200 rounded-full hover:bg-blue-200">
              <ArrowBigLeft className="h-6 w-6 text-gray-800" />
            </div>
            <span>Voltar</span>
          </button>
        </div>
      </nav>

      <div className="max-w-2xl mx-auto p-6 space-y-6">
        <div className="bg-white rounded-2xl shadow p-6 space-y-4">
          <h2 className="text-xl font-semibold">Editar Perfil</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Input
              placeholder="Nome"
              value={editUser.name}
              onChange={(e) =>
                setEditUser({ ...editUser, name: e.target.value })
              }
            />
            <Input
              placeholder="Nome de usuário"
              value={editUser.userName}
              onChange={(e) =>
                setEditUser({ ...editUser, userName: e.target.value })
              }
            />
            <Input
              placeholder="Nova Senha"
              type="password"
              value={editUser.password}
              onChange={(e) =>
                setEditUser({ ...editUser, password: e.target.value })
              }
            />
          </div>
          <Button onClick={updateUserProfile}>Salvar Alterações</Button>
        </div>
      </div>
    </main>
  );
}
