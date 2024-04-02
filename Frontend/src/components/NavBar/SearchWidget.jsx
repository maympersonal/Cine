// import React, { useState, useEffect, useRef } from "react";
// import axios from "../../api/axios"; // Ajusta la ruta a tu configuración de axios
// import SearchDropdown from "./SearchDropdown";

// const SearchWidget = ({ btnStyles }) => {
//     const [searchTerm, setSearchTerm] = useState("");
//     const [showDropdown, setShowDropdown] = useState(false);
//     const [searchResults, setSearchResults] = useState([]);
//     const searchRef = useRef(null);

//     useEffect(() => {
//         const handleClickOutside = (event) => {
//             if (searchRef.current && !searchRef.current.contains(event.target)) {
//                 setShowDropdown(false);
//             }
//         };

//         document.addEventListener("mousedown", handleClickOutside);
//         return () => document.removeEventListener("mousedown", handleClickOutside);
//     }, [searchRef]);

//     useEffect(() => {
//         if (searchTerm.trim() !== "") {
//             axios.get(`Pelicula/GetAll?titulo=${searchTerm}`)
//                 .then((res) => {
//                     setSearchResults(res.data);
//                     setShowDropdown(true);
//                 })
//                 .catch((err) => console.log(err));
//         } else {
//             setSearchResults([]);
//             setShowDropdown(false);
//         }
//     }, [searchTerm]);

//     return (
//         <div ref={searchRef} className="relative">
//             <input
//                 type="text"
//                 placeholder="Buscar..."
//                 className="input input-bordered"
//                 value={searchTerm}
//                 onChange={(e) => setSearchTerm(e.target.value)}
//                 onFocus={() => searchTerm.trim() !== "" && setShowDropdown(true)}
//             />
//             {showDropdown && <SearchDropdown searchResults={searchResults} />}
//         </div>
//     );
// };

// export default SearchWidget;


import { useEffect } from "react";
import { useRef } from "react";
import { useState } from "react";
import SearchDropdown from "./SearchDropdown";

const SearchWidget = ({ btnStyles }) => {
    const [searchTerm, setSearchTerm] = useState('');

    const [open, setOpen] = useState(false);
    const openSearcher = () => {
        setOpen(true);
    }

    const closeSearcher = () => {
        openedRef.current.classList.remove('slide-in-left');
        openedRef.current.classList.add('slide-out-right');
        setTimeout(() => {
            openedRef.current.classList.add('hidden');
            inputRef.current.value = '';
            setSearchTerm('');
            setOpen(false);
        }, 400);
    }

    const inputRef = useRef();
    useEffect(() => {
        if (open) { // Si está abierto
            // Hago visible el div
            openedRef.current.classList.remove('slide-out-right');
            openedRef.current.classList.remove('hidden');
            openedRef.current.classList.add('slide-in-left');

            // Hago focus en el input
            inputRef.current.focus();
        }
    }, [open])

    const openedRef = useRef();
    const submitBusqueda = ({ target }) => {
        setSearchTerm(target.value);
    }

    return (
        <div className="uppercase">
            <div className="searchContainerOverlay py-5 px-4 text-xs xs:text-sm sm:text-xl sm:px-10 md:text-xl md:py-2 xl:text-xl hidden" ref={openedRef}>
                <div className="searchContainer text-black">

                    <input type="search" placeholder="INGRESA TU BÚSQUEDA" className="searchContainer_input" ref={inputRef} onChange={submitBusqueda}/>

                    <div className={btnStyles}>
                        <button><i className="fa-solid fa-magnifying-glass"></i></button>
                    </div>

                    <div className={btnStyles} onClick={closeSearcher}>
                        <button><i className="fa-solid fa-xmark"></i></button>
                    </div>

                </div>

                {searchTerm &&
                    <SearchDropdown searchTerm={searchTerm} close={closeSearcher}/>
                }

            </div>

            <div className={btnStyles} onClick={openSearcher} >
                <button><i className="fa-solid fa-magnifying-glass"></i></button>
            </div>
        </div>
    )
}

export default SearchWidget;