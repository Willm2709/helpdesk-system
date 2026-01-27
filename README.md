# 🛠️ Helpdesk System

Sistema de chamados (Helpdesk) desenvolvido em **C#**, com aplicação de **Programação Orientada a Objetos (POO)** e persistência de dados via arquivos **JSON**.  
Este projeto é um **MVP (Produto Mínimo Viável)** para fins de **aprendizado e portfólio profissional**.

---

## 💡 Funcionalidades

### 👤 Usuários
- Cadastro de usuários com perfil comum ou suporte
- Login com verificação de credenciais
- Logout

### 🎫 Chamados
- Abertura de chamados
- Listagem de chamados (usuário vê somente os seus)
- Encerramento de chamados
- Cada chamado é associado ao usuário que o criou

### 🧑‍💼 Modo Suporte
- Usuários do tipo suporte podem:
  - Ver **todos os chamados do sistema**
  - Ajudar no gerenciamento e encerramento

### 💾 Persistência
- Dados de usuários e chamados são salvos automaticamente em arquivos `.json`
- Dados são carregados automaticamente ao iniciar o programa

---

## 🚀 Como executar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/Willm2709/helpdesk-system.git
```

2. Abra no Visual Studio

3. Execute com `F5` ou `Ctrl + F5`

---

## 📦 Versões

### 🔖 v1.0
- MVP com funcionalidades básicas (cadastro, login, abertura/listagem de chamados)

### 🚀 v2.0 (Atual)
- Implementado sistema de perfis (usuário/suporte)
- Adicionado modo suporte para visualizar todos os chamados
- Persistência em arquivos JSON
- Filtragem de chamados por usuário

---

## 📌 Requisitos

- .NET 6 ou superior
- Visual Studio 2022 ou superior

---

## 🧠 Aprendizados aplicados

- Estruturação de sistemas via console
- Programação orientada a objetos (POO)
- Serialização/Deserialização com `System.Text.Json`
- Separação de responsabilidades por tipo de usuário
- Lógica de menus e fluxo de sistema

---

## ✨ Autor

William Moreira – [@Willm2709](https://github.com/Willm2709)