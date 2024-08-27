const decrementButtons = document.querySelectorAll('.quantity button:first-child');
const incrementButtons = document.querySelectorAll('.quantity button:last-child');
const quantitySpans = document.querySelectorAll('.quantity span');
const totalPriceElements = document.querySelectorAll('p.total');

// Обработчики событий
decrementButtons.forEach((button, index) => {
    button.addEventListener('click', () => {
        updateQuantity(index, -1);
    });
});

incrementButtons.forEach((button, index) => {
    button.addEventListener('click', () => {
        updateQuantity(index, 1);
    });
});

// Функция для обновления количества и общей стоимости
function updateQuantity(index, change) {
    let quantity = parseInt(quantitySpans[index].textContent);
    quantity += change;
    if (quantity >= 1) {
        quantitySpans[index].textContent = quantity;
        updateTotalPrice(index, quantity);
    }
}

// Функция для обновления общей стоимости
function updateTotalPrice(index, quantity) {
    const price = parseFloat(totalPriceElements[index].dataset.price);
    const totalPrice = price * quantity;
    totalPriceElements[index].textContent = `Total: ${totalPrice.toFixed(2)}`;
}