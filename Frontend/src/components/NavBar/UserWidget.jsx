// import React, { useContext } from "react";
// import { Link } from "react-router-dom";
// import { useUser } from "../../context/UserContext";

// const UserWidget = () => {
//     const { user, logout, isLogged } = useUser();

//     return (
//         <div className="userWidget">
//             {isLogged ? (
//                 <div className="flex items-center gap-2">
//                     <span>Hola, {user.nombreS}</span>
//                     <button onClick={logout} className="btn btn-ghost">
//                         Logout
//                     </button>
//                 </div>
//             ) : (
//                 <Link to="/login" className="btn btn-ghost">
//                     Login
//                 </Link>
//             )}
//         </div>
//     );
// };

// export default UserWidget;




import { useState } from "react";
import { Link } from "react-router-dom";
import { useUser } from '../../context/UserContext'
import UserFormContainer from "../UserForm/UserFormContainer";
import MovieAddFormContainer from "../New/MovieAddFormContainer";

const UserWidget = ({ btnStyles }) => {
    const { isLogged, logout, userWidgetRef, user } = useUser();
    const [openForm, setOpenForm] = useState(false);
    const [wichForm, setWichForm] = useState();

    const seeForm = (e) => {
        setWichForm(e.target.innerText);
        setOpenForm(true);
    }

    const closeForm = () => {
        setOpenForm(false);
    }

    const closeDropDown = (e) => {
        e.target.blur();
    }

    return (
        <div>
            {openForm && <UserFormContainer open={openForm} closeForm={closeForm} wichForm={wichForm}/>}
            {(openForm&&wichForm==="Añadir Pelicula")?<MovieAddFormContainer open={openForm} closeForm={closeForm}/>:null}
            <div className="dropdown dropdown-end">
                <label tabIndex={0} className={btnStyles} ref={userWidgetRef}>
                    <button><i className="fa-regular fa-user"></i></button>
                </label>

                {isLogged ?
                    <ul tabIndex={0} className="mt-3 p-2 shadow menu menu-compact dropdown-content bg-base-100 rounded-box w-52 text-black font-albert font-semibold">
                        
                        <li className="text-lg text-center btn btn-primary pointer-events-none">{user.nombreS}</li>
                        {user.rol==="Admin"?<li onClick={closeDropDown}> <button onClick={seeForm} className='text-lg'> Añadir Pelicula </button> </li>:null}
                        {(user.rol==="Taquillero"||user.rol==="Admin")?<li onClick={closeDropDown}> <Link to={'/taquillero/confiabilidad'} className='text-lg'> Usuarios </Link> </li>:null}
                        <li onClick={closeDropDown}> <Link to={'/user/tickets'} className='text-lg'> Mis tickets </Link> </li>
                        <li onClick={closeDropDown}> <button onClick={logout} className='text-lg'> Salir </button> </li>
                    </ul>
                    :
                    <ul tabIndex={0} className="mt-3 p-2 shadow menu menu-compact dropdown-content bg-base-100 rounded-box w-52 text-black font-albert font-semibold">
                        <li onClick={closeDropDown}> <button onClick={seeForm} className='text-lg'> Registrarse </button> </li>
                        <li onClick={closeDropDown}> <button onClick={seeForm} className='text-lg'> Ingresar </button> </li>
                    </ul>

                }

            </div>
        </div>
    )
}

export default UserWidget;