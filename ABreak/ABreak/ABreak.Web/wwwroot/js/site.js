function confirmarExclusao(mensagem) {
    return confirm(mensagem || 'Tem certeza que deseja excluir este item?');
}

// Exemplo: notificação simples
function mostrarNotificacao(mensagem, tipo = 'info') {
    alert(mensagem);
}