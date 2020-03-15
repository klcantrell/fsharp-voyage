module ReactBeautifulDnd

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Fable.React

// DRAG CONTEXT

type DroppableMetadata = { droppableId: string }

type DragEndResult = { 
    source: DroppableMetadata
    destination: DroppableMetadata
}

type DragDropContextProps = OnDragEnd of (DragEndResult -> unit)

let inline DragDropContext (props: DragDropContextProps list) (elements: ReactElement list): ReactElement =
    ofImport "DragDropContext" "react-beautiful-dnd" (keyValueList CaseRules.LowerFirst props) elements

// DROPPABLE

type DroppableProvided = {
    innerRef: Element -> unit
    placeholder: ReactElement
}

type DroppableSnapshot = { isDraggingOver: bool }

type DroppableProps = 
    | DroppableId of string
    | Children of (DroppableProvided -> DroppableSnapshot -> ReactElement)

let inline Droppable (props: DroppableProps list): ReactElement =
    ofImport "Droppable" "react-beautiful-dnd" (keyValueList CaseRules.LowerFirst props) []

// DRAGGABLE

type ProvidedDraggableProps = {
    style: obj
}

type DraggableProvided = {
    innerRef: Element -> unit
    draggableProps: ProvidedDraggableProps
    dragHandleProps: obj
}

type DraggableSnapshot = { isDragging: bool }

type DraggableProps =
    | DraggableId of string
    | Index of int
    | Children of (DraggableProvided -> DraggableSnapshot -> unit)
