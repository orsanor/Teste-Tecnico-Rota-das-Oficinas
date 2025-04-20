import { cn } from "@/lib/utils";
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";

interface RegisterFormProps extends React.ComponentPropsWithoutRef<"div"> {
  form: {
    userName: string;
    name: string;
    email: string;
    password: string;
    passwordConfirmation: string;
  };
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onSubmit: (e: React.FormEvent) => void;
  error?: string;
  onBackClick: () => void;
}

export function RegisterForm({
  className,
  form,
  onChange,
  onSubmit,
  error,
  onBackClick,
  ...props
}: RegisterFormProps) {
  return (
    <div className={cn("flex flex-col gap-6", className)} {...props}>
      <Card className="w-full max-w-md shadow-xl border border-gray-200">
        <CardHeader>
          <CardTitle className="text-center text-2xl font-bold text-gray-800">
            Criar Conta
          </CardTitle>
          <CardDescription className="text-center">
            Preencha os dados abaixo para se registrar
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={onSubmit} className="space-y-4">
            <div className="space-y-1">
              <Label htmlFor="userName">Usuário</Label>
              <Input
                id="userName"
                name="userName"
                placeholder="Nome de usuário"
                value={form.userName}
                onChange={onChange}
              />
            </div>
            <div className="space-y-1">
              <Label htmlFor="name">Nome completo</Label>
              <Input
                id="name"
                name="name"
                placeholder="Seu nome"
                value={form.name}
                onChange={onChange}
              />
            </div>
            <div className="space-y-1">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                name="email"
                type="email"
                placeholder="Seu email"
                value={form.email}
                onChange={onChange}
              />
            </div>
            <div className="space-y-1">
              <Label htmlFor="password">Senha</Label>
              <Input
                id="password"
                name="password"
                type="password"
                placeholder="Crie uma senha"
                value={form.password}
                onChange={onChange}
              />
            </div>
            <div className="space-y-1">
              <Label htmlFor="passwordConfirmation">Confirmar Senha</Label>
              <Input
                id="passwordConfirmation"
                name="passwordConfirmation"
                type="password"
                placeholder="Confirme a senha"
                value={form.passwordConfirmation}
                onChange={onChange}
              />
            </div>
            {error && <p className="text-sm text-red-500">{error}</p>}
            <Button
              type="submit"
              className="w-full cursor-pointer hover:bg-blue-600 hover:text-white transition-colors duration-200 hover:scale-[1.02]"
            >
              Registrar
            </Button>

            <Button
              type="button"
              variant="outline"
              className="w-full cursor-pointer transition-transform hover:scale-[1.02]"
              onClick={onBackClick}
            >
              Voltar ao Login
            </Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}