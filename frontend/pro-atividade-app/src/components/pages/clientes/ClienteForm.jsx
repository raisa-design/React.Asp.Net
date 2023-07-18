import React from 'react';
//import TitlePage from './../../TitlePage';
import TitlePage from '../../../components/TitlePage';
import { Button } from 'react-bootstrap';
import { useHistory, useParams } from 'react-router-dom/cjs/react-router-dom.min';

export default function ClienteForm() {
  let history = useHistory();
  let {id} = useParams();

  return(
    <>
      <TitlePage title={'Cliente Detalhes' + (id !== undefined ? id: '')}
      >
         <Button
             variant='outline-secondary'
             onClick={() => history.goBack()} //goBack ou push('/cliente/lista')}
         >
             <i className='fas fa-arrow-left me-2'></i>
              Voltar
         </Button>
      </TitlePage>
      <div></div>
    </>
  );
}
