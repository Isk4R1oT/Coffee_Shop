const productPrice = document.querySelector('.product-price');
const sizeBtns = document.querySelectorAll('.size-btn');
const addToCartBtn = document.querySelector('.add-to-cart');

let originalPrice = parseFloat(productPrice.textContent);
let selectedSize = 'XL'; // Установите начальный выбранный размер

sizeBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        selectedSize = btn.dataset.size;
        updatePrice();
    });
});

function updatePrice() {
    let newPrice = originalPrice;

    switch (selectedSize) {
        case 'L':
            newPrice = Math.ceil(originalPrice * 0.75);
            break;
        case 'XL':
            // Цена остается без изменений
            break;
        case 'XXL':
            newPrice = Math.ceil(originalPrice * 1.25);
            break;
    }

    productPrice.textContent = newPrice;
}

addToCartBtn.addEventListener('click', () => {
    addToCart(selectedSize, newPrice);
});

