// Import styles.
import './design.css';

// Load html content.
import * as htmlContent from './index.html';

// Export generic functions.
export * from './common.js';

// Display html content.
document.body.innerHTML = htmlContent;

// Generate scripts.
const jsDemoImagesTransform = document.createElement('script');
jsDemoImagesTransform.type = 'text/javascript';
jsDemoImagesTransform.src = 'https://www.wiris.net/demo/plugins/app/WIRISplugins.js?viewer=image';

// Load generated scripts.
document.head.appendChild(jsDemoImagesTransform);
