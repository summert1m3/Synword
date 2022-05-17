import Modal, { useModalState } from "react-simple-modal-provider";
import "./modal.scss";
import PickSynonymsModalBody from "./PickSynonymsModalBody";

export default ({ children }) => {
  const [isOpen, setOpen] = useModalState();

  return (
    <Modal id={"PickSynonymsModal"} consumer={children} isOpen={isOpen} setOpen={setOpen}>
      <div className="modal-body">
        <PickSynonymsModalBody/>
      </div>
    </Modal>
  );
};
