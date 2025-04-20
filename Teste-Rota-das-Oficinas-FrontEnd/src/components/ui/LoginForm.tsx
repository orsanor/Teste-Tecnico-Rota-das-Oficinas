// components/LoginForm.tsx

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface LoginFormProps extends React.ComponentPropsWithoutRef<"div"> {
  form: {
    userName: string;
    password: string;
  };
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onSubmit: (e: React.FormEvent) => void;
  error?: string;
  onRegisterClick: () => void;
}

export function LoginForm({
  className,
  form,
  onChange,
  onSubmit,
  error,
  onRegisterClick,
  ...props
}: LoginFormProps) {
  return (
    <div className={cn("flex flex-col gap-6", className)} {...props}>
      <Card className="w-full max-w-md shadow-xl border border-gray-200">
        <CardHeader>
          <CardTitle className="text-center text-2xl font-bold text-gray-800">
            Rota das Oficinas
          </CardTitle>
          <CardDescription className="text-center">
            Entre com seu usuário e senha
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={onSubmit} className="space-y-4">
            <div className="space-y-1">
              <Label htmlFor="userName">Usuário</Label>
              <Input
                id="userName"
                name="userName"
                placeholder="Insira seu usuário"
                value={form.userName}
                onChange={onChange}
              />
            </div>
            <div className="space-y-1">
              <Label htmlFor="password">Senha</Label>
              <Input
                type="password"
                id="password"
                name="password"
                placeholder="Senha"
                value={form.password}
                onChange={onChange}
              />
            </div>
            {error && <p className="text-sm text-red-500">{error}</p>}
            <Button
              type="submit"
              className="w-full cursor-pointer hover:bg-blue-600 hover:text-white transition-colors duration-200 hover:scale-[1.02]"
            >
              Entrar
            </Button>
            <Button
              type="button"
              variant="outline"
              className="w-full cursor-pointer transition-transform hover:scale-[1.02]"
              onClick={onRegisterClick}
            >
              Registrar
            </Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}