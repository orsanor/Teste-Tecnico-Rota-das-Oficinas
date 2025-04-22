import { useState, useEffect } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Dialog, DialogContent, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Pencil, Trash2 } from "lucide-react";
import api from "../../services/api";

export default function HomePage() {
  const [products, setProducts] = useState<{ id: number; name: string; price: number; quantity: number; description: string }[]>([]);
  const [newProduct, setNewProduct] = useState({ name: "", price: "", quantity: "", description: "" });
  const [editingIndex, setEditingIndex] = useState(null);
  const [editProduct, setEditProduct] = useState({ name: "", price: "", quantity: "", description: "" });

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const res = await api.get("/product?page=1&size=10");
      setProducts(res.data.data || []);
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
    }
  };

  const deleteProduct = async (id: number) => {
    console.log("Deleting product with ID:", id);
    await api.delete(`/product/${id}`);
    setProducts(products.filter((product) => product.id !== id));
  };

  const startEditing = (index) => {
    setEditingIndex(index);
    setEditProduct(products[index]);
  };

  const saveEdit = async () => {
    const res = await api.put(`/product/${editProduct.id}`, {
      ...editProduct,
      price: Number(editProduct.price),
      quantity: Number(editProduct.quantity)
    });
    const updated = [...products];
    updated[editingIndex] = res.data;
    setProducts(updated);
    setEditingIndex(null);
    setEditProduct({ name: "", price: "", quantity: "", description: "" });
    fetchProducts(); 
  };

  return (
    <main className="min-h-screen bg-gray-50">
      {/* Navbar */}
      <nav className="bg-white shadow p-4 mb-6">
        <div className="max-w-7xl mx-auto flex items-center justify-between">
          <h1 className="text-2xl font-bold text-gray-800">Gerenciador de Produtos</h1>
        </div>
      </nav>

      <div className="max-w-2xl mx-auto p-6 space-y-6">
        {/* Formulário de Cadastro */}
        <div className="bg-white rounded-2xl shadow p-6 space-y-4">
          <h2 className="text-xl font-semibold">Cadastrar Produto</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Input
              placeholder="Nome"
              value={newProduct.name}
              onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}
            />
            <Input
              placeholder="Preço"
              type="number"
              value={newProduct.price}
              onChange={(e) => setNewProduct({ ...newProduct, price: e.target.value })}
            />
            <Input
              placeholder="Quantidade"
              type="number"
              value={newProduct.quantity}
              onChange={(e) => setNewProduct({ ...newProduct, quantity: e.target.value })}
            />
            <Input
              placeholder="Descrição"
              value={newProduct.description}
              onChange={(e) => setNewProduct({ ...newProduct, description: e.target.value })}
            />
          </div>
          <Button onClick={addProduct}>Adicionar Produto</Button>
        </div>

        {products.length > 0 && (
          <div className="grid gap-4">
            {products.map((product, index) => (
              <Card key={product.id} className="p-4">
                <CardContent className="p-0">
                  <div className="flex justify-between items-center">
                    <div>
                      <h3 className="text-lg font-bold">{product.name}</h3>
                      <p className="text-sm text-gray-600">{product.description}</p>
                      <p className="text-sm">Preço: R$ {product.price}</p>
                      <p className="text-sm">Quantidade: {product.quantity}</p>
                    </div>
                    <div className="flex gap-2">
                      <Dialog>
                        <DialogTrigger asChild>
                          <Button variant="outline" size="icon" className="cursor-pointer" onClick={() => startEditing(index)}>
                            <Pencil className="w-4 h-4" />
                          </Button>
                        </DialogTrigger>
                        <DialogContent style={{ backgroundColor: "white" }}>
                          <DialogTitle>Editar Produto</DialogTitle>
                          <div className="space-y-3">
                            <Input
                              placeholder="Nome"
                              value={editProduct.name}
                              onChange={(e) => setEditProduct({ ...editProduct, name: e.target.value })}
                            />
                            <Input
                              placeholder="Preço"
                              type="number"
                              value={editProduct.price}
                              onChange={(e) => setEditProduct({ ...editProduct, price: e.target.value })}
                            />
                            <Input
                              placeholder="Quantidade"
                              type="number"
                              value={editProduct.quantity}
                              onChange={(e) => setEditProduct({ ...editProduct, quantity: e.target.value })}
                            />
                            <Input
                              placeholder="Descrição"
                              value={editProduct.description}
                              onChange={(e) => setEditProduct({ ...editProduct, description: e.target.value })}
                            />
                            <Button onClick={saveEdit} className="cursor-pointer">Salvar</Button>
                          </div>
                        </DialogContent>
                      </Dialog>
                      <Button variant="destructive" size="icon" className="cursor-pointer" onClick={() => deleteProduct(product.id)}>
                        <Trash2 className="w-4 h-4" color="red" />
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </div>
    </main>
  );
}