// DESAFIO: Sistema de Chat em Grupo
// PROBLEMA: Um aplicativo de mensagens tem usuários que precisam enviar mensagens para grupos,
// notificar quando entram/saem, e gerenciar permissões.
// O código anterior fazia cada usuário conhecer e se comunicar diretamente com todos os outros, criando acoplamento complexo

using DesignPatternChallenge_Iterator.Components;
using DesignPatternChallenge_Iterator.Mediator;

namespace DesignPatternChallenge_Mediator;

internal class Program
{
    // Contexto: Sistema de chat onde usuários se comunicam em grupos

    static void Main(string[] args)
    {
        Console.WriteLine("=== Sistema de Chat em Grupo ===\n");

        var mediator = new ChatMediator();
        var group01 = new ChatGroup("Grupo de Amigos");
        var group02 = new ChatGroup("Clube do Bolinha");


        // Criando usuários
        var alice = new ChatUser("Alice", mediator);
        var bob = new ChatUser("Bob", mediator);
        var carlos = new ChatUser("Carlos", mediator);
        var diana = new ChatUser("Diana", mediator);
        var eric = new ChatUser("Eric", mediator);
        var fiona = new ChatUser("Fiona", mediator);
        var gabriel = new ChatUser("Gabriel", mediator);
        var helena = new ChatUser("Helena", mediator);

        Console.WriteLine("=== Usuários Entrando no Grupo de Amigos ===");
        alice.JoinGroup(group01);
        bob.JoinGroup(group01);
        carlos.JoinGroup(group01);
        diana.JoinGroup(group01);
        eric.JoinGroup(group01);
        fiona.JoinGroup(group01);
        gabriel.JoinGroup(group01);
        helena.JoinGroup(group01);

        Console.WriteLine("\n=== Usuários Entrando no Clube do Bolinha ===");
        bob.JoinGroup(group02);
        carlos.JoinGroup(group02);
        eric.JoinGroup(group02);
        gabriel.JoinGroup(group02);

        Console.WriteLine("\n=== Conversação no Grupo de Amigos ===");
        alice.SendMessage("Olá, pessoal!", group01);
        bob.SendMessage("Oi, Alice!", group01);
        carlos.SendMessage("E aí!", group01);

        Console.WriteLine("\n=== Mensagem Privada ===");
        alice.SendPrivateMessage(bob, "Bob, você viu o relatório?");
        bob.SendPrivateMessage(alice, "Vi sim, está ótimo! Obrigado!");

        Console.WriteLine("\n=== Moderação Grupo de Amigos ===");
        alice.MuteUser(carlos, group01);
        carlos.SendMessage("Ainda posso falar?", group01); // Não será enviado
        alice.MuteUser(carlos, group01); // Não pode ser mutado 2x

        Console.WriteLine("\n=== Saindo do Grupo ===");
        diana.LeaveGroup(group01);
        alice.SendMessage("Pq a Diana saiu?", group01);

        Console.WriteLine("\n=== Conversação no Clube do Bolinha ===");
        carlos.SendMessage("Acredita que me mutaram no outro grupo? Hahaha", group02);
        gabriel.SendMessage("Poxa, Carlos. O que aconteceu?", group02);
        carlos.SendMessage("Alice me mutou, mas não sei o motivo hahaha", group02);

        Console.WriteLine("\n=== Moderação Grupo de Amigos ===");
        alice.UnmuteUser(carlos, group01);
        alice.SendMessage("Carlos, me desculpe, te mutei sem querer!", group01);
        carlos.SendMessage("Tudo bem, Alice! Sem problemas!", group01);

        helena.UnmuteUser(fiona, group01); // Fiona não está mutada, então não pode ser desmutada
    }
}