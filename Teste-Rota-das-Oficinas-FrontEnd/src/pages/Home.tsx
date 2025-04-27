import { useState, useEffect } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogContent,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Pencil, Trash2, LogOut } from "lucide-react";
import api from "../../services/api";
import { UserIcon } from "@heroicons/react/outline";
import { useNavigate } from "react-router-dom";

export default function HomePage() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [products, setProducts] = useState<
    {
      id: number;
      name: string;
      price: number;
      quantity: number;
      description: string;
    }[]
  >([]);
  const [newProduct, setNewProduct] = useState({
    name: "",
    price: "",
    quantity: "",
    description: "",
  });
  const [editingIndex, setEditingIndex] = useState(null);
  const [editProduct, setEditProduct] = useState<{
    id?: number;
    name: string;
    price: string;
    quantity: string;
    description: string;
  }>({ name: "", price: "", quantity: "", description: "" });

  const fetchProducts = async () => {
    try {
      const res = await api.get("/product?page=1&size=10");
      setProducts((res.data as { data: { id: number; name: string; price: number; quantity: number; description: string; }[] }).data || []);
    } catch (error) {
      console.error("Erro ao buscar produtos:", error);
    }
  };

  const addProduct = async () => {
    if (newProduct.name.trim()) {
      await api.post("/product", {
        ...newProduct,
        price: Number(newProduct.price),
        quantity: Number(newProduct.quantity),
      });
      setNewProduct({ name: "", price: "", quantity: "", description: "" });
      fetchProducts();
      setOpen(false);
    }
  };

  const deleteProduct = async (id: number) => {
    console.log("Deleting product with ID:", id);
    await api.delete(`/product/${id}`);
    setProducts(products.filter((product) => product.id !== id));
  };

  const startEditing = (id: number) => {
    const productToEdit = products.find((product) => product.id === id);
    if (productToEdit) {
      setEditingIndex(id as unknown as null);
      setEditProduct({
        id: productToEdit.id,
        name: productToEdit.name,
        price: productToEdit.price.toString(),
        quantity: productToEdit.quantity.toString(),
        description: productToEdit.description
      });
    }
  };

  const goToProfile = () => {
    navigate("/Users");
  };

  const saveEdit = async () => {
    const res = await api.put(`/product/${editProduct.id}`, {
      ...editProduct,
      price: Number(editProduct.price),
      quantity: Number(editProduct.quantity),
    });
    const updated = [...products];
    updated[products.findIndex((p) => p.id === editingIndex)] = res.data as { id: number; name: string; price: number; quantity: number; description: string; };
    setProducts(updated);
    setEditingIndex(null);
    setEditProduct({ name: "", price: "", quantity: "", description: "" });
    fetchProducts();
  };


  useEffect(() => {
    fetchProducts();
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/login');
  };

  return (
    <main className="min-h-screen bg-gray-50">
      <nav className="bg-white shadow p-4 mb-6">
        <div className="max-w-7xl mx-auto flex items-center justify-between">
          <h1 className="text-2xl font-bold text-gray-800">
            Gerenciador de Produtos
          </h1>
          <div className="flex items-center gap-4">
            <button
              onClick={goToProfile}
              className="flex items-center space-x-2 text-gray-800 hover:text-blue-600 transition-colors duration-200 cursor-pointer"
            >
              <div className="flex items-center justify-center w-10 h-10 bg-gray-200 rounded-full hover:bg-blue-200">
                <UserIcon className="h-6 w-6 text-gray-800" />
              </div>
            </button>
            <button
              onClick={handleLogout}
              className="flex items-center space-x-2 text-gray-800 hover:text-red-600 transition-colors duration-200 cursor-pointer"
            >
              <div className="flex items-center justify-center w-10 h-10 bg-gray-200 rounded-full hover:bg-red-200">
                <LogOut className="h-6 w-6 text-gray-800" />
              </div>
            </button>
          </div>
        </div>
      </nav>

      <div className="max-w-4xl mx-auto p-4 md:p-8">
        <div className="bg-white rounded-xl shadow-xl md:p-8 p-8 space-y-6">
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-2xl font-semibold text-gray-800">
              Produtos Cadastrados
            </h2>
            <Dialog open={open} onOpenChange={setOpen}>
              <DialogTrigger>
                <button className="bg-blue-600 text-white py-3 px-6 rounded-lg hover:bg-blue-700 transition-all focus:outline-none focus:ring-2 focus:ring-blue-500">
                  Cadastre um novo produto
                </button>
              </DialogTrigger>

              <DialogContent className="bg-white p-6 rounded-lg shadow-lg max-w-lg mx-auto space-y-6">
                <DialogTitle className="text-xl font-semibold text-gray-800">
                  Adicionar Produto
                </DialogTitle>
                <div className="space-y-4">
                  <Input
                    placeholder="Nome do Produto"
                    value={newProduct.name}
                    onChange={(e) =>
                      setNewProduct({ ...newProduct, name: e.target.value })
                    }
                    className="w-full p-3 border rounded-lg border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-all"
                  />
                  <Input
                    placeholder="Preço"
                    type="number"
                    value={newProduct.price}
                    onChange={(e) =>
                      setNewProduct({ ...newProduct, price: e.target.value })
                    }
                    className="w-full p-3 border rounded-lg border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-all"
                  />
                  <Input
                    placeholder="Quantidade"
                    type="number"
                    value={newProduct.quantity}
                    onChange={(e) =>
                      setNewProduct({ ...newProduct, quantity: e.target.value })
                    }
                    className="w-full p-3 border rounded-lg border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-all"
                  />
                  <Input
                    placeholder="Descrição"
                    value={newProduct.description}
                    onChange={(e) =>
                      setNewProduct({
                        ...newProduct,
                        description: e.target.value,
                      })
                    }
                    className="w-full p-3 border rounded-lg border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-all"
                  />
                  <Button
                    onClick={addProduct}
                    className="w-full bg-blue-600 text-white py-3 rounded-lg hover:bg-blue-700 transition-all focus:outline-none"
                  >
                    Adicionar Produto
                  </Button>
                </div>
              </DialogContent>
            </Dialog>
          </div>

          {products.length > 0 ? (
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-2">
              {products.map((product) => (
                <Card
                  key={product.id}
                  className="p-4 border rounded-lg shadow-lg"
                >
                  <CardContent className="p-0">
                    <div className="flex justify-between items-center">
                      <div>
                        <h3 className="text-lg font-bold">{product.name}</h3>
                        <p className="text-sm text-gray-600">
                          {product.description}
                        </p>
                        <p className="text-sm">Preço: R$ {product.price}</p>
                        <p className="text-sm">
                          Quantidade: {product.quantity}
                        </p>
                      </div>
                      <div className="flex gap-2">
                        <Dialog>
                          <DialogTrigger>
                            <Button
                              variant="outline"
                              size="icon"
                              onClick={() => startEditing(product.id)}
                              className="cursor-pointer"
                            >
                              <Pencil className="w-4 h-4" />
                            </Button>
                          </DialogTrigger>
                          <DialogContent className="bg-white p-6 rounded-lg shadow-lg">
                            <DialogTitle>Editar Produto</DialogTitle>
                            <div className="space-y-3">
                              <Input
                                placeholder="Nome"
                                value={editProduct.name}
                                onChange={(e) =>
                                  setEditProduct({
                                    ...editProduct,
                                    name: e.target.value,
                                  })
                                }
                              />
                              <Input
                                placeholder="Preço"
                                type="number"
                                value={editProduct.price}
                                onChange={(e) =>
                                  setEditProduct({
                                    ...editProduct,
                                    price: e.target.value,
                                  })
                                }
                              />
                              <Input
                                placeholder="Quantidade"
                                type="number"
                                value={editProduct.quantity}
                                onChange={(e) =>
                                  setEditProduct({
                                    ...editProduct,
                                    quantity: e.target.value,
                                  })
                                }
                              />
                              <Input
                                placeholder="Descrição"
                                value={editProduct.description}
                                onChange={(e) =>
                                  setEditProduct({
                                    ...editProduct,
                                    description: e.target.value,
                                  })
                                }
                              />
                              <Button
                                onClick={saveEdit}
                                className="w-full bg-blue-600 text-white py-3 rounded-lg hover:bg-blue-700 transition-all"
                              >
                                Salvar
                              </Button>
                            </div>
                          </DialogContent>
                        </Dialog>
                        <Button
                          variant="destructive"
                          size="icon"
                          onClick={() => deleteProduct(product.id)}
                          className="cursor-pointer"
                        >
                          <Trash2 className="w-4 h-4" color="red" />
                        </Button>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          ) : (
            <p className="text-center text-gray-600">
              Nenhum produto cadastrado.
            </p>
          )}
        </div>
      </div>
    </main>
  );
}
