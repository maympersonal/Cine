// tailwind.config.js

import daisyUI from 'daisyui';

export default {
    content: [
        "./index.html",
        "./src/**/*.{vue,js,ts,jsx,tsx}",
    ],
    theme: {
        container: {
            center: true,
            padding: {
                DEFAULT: '0',
                sm: '0',
                lg: '0',
                xl: '9rem',
                '2xl': '12rem',
            },
        },
        screens: {
            'xxs': '475px',
            'xs': '576px',
            'sm': '640px',
            'md': '768px',
            'mlg': '960px',
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1536px',
            '3xl': '1600px',
        },
        extend: {
            fontFamily: {
                sans: ['Albert Sans', 'sans-serif'],
            },
        },
    },
    plugins: [
        daisyUI,
    ],
    daisyui: {
        themes: [
            {
                mytheme: {
                    "primary": "#570df8",
                    "secondary": "#f000b8",
                    "accent": "#37cdbe",
                    "neutral": "#3d4451",
                    "base-100": "#ffffff",
                    // Define other colors...
                },
            },
            "light",
            "dark",
        ],
    },
};



// /** @type {import('tailwindcss').Config} */

// export default {
//     content: [
//         "./index.html",
//         "./src/**/*.{vue,js,ts,jsx,tsx}",
//     ],
//     theme: {
//         container: {
//             center: true,
//             padding: {
//                 DEFAULT: '0',
//                 sm: '0',
//                 lg: '0',
//                 xl: '9rem',
//                 '2xl': '12rem'
//             }
//         },
//         screens: {
//             'xxs': '475px',

//             'xs': '576px',

//             'sm': '640px',
//             // => @media (min-width: 640px) { ... }

//             'md': '768px',
//             // => @media (min-width: 768px) { ... }

//             'mlg': '960px',

//             'lg': '1024px',
//             // => @media (min-width: 1024px) { ... }

//             'xl': '1280px',
//             // => @media (min-width: 1280px) { ... }

//             '2xl': '1536px',
//             // => @media (min-width: 1536px) { ... }
//         },
//         extend: {
//             screens: {
//                 '3xl': '1600px',
//             },
//             fontFamily: {
//                 sans: ['Albert Sans', 'sans-serif'],
//             }
//         },
//     },
//     daisyui: {
//         themes: [
//             {
//                 light: {
//                     ...require("/node_modules/daisyui/src/theming/themes.js")["[data-theme=light]"],
//                     primary: "#9D0208",
//                 },
//             },
//         ],
//     },
//     plugins: [
//         require('daisyui'),
//     ],
// }